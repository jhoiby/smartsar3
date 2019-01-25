using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSar.Contexts.Common.Application.IntegrationEvents;
using SSar.Contexts.Common.Application.ServiceInterfaces;
using SSar.Contexts.Common.Data.Extensions;
using SSar.Contexts.Common.Data.Outbox;
using SSar.Contexts.Common.Data.ServiceInterfaces;
using SSar.Contexts.Common.Data.TypeConfigurations;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.ServiceInterfaces;
using SSar.Contexts.IdentityAuth.Domain.Entities;
using SSar.Contexts.Membership.Domain.Entities.ExamplePersons;

namespace SSar.Contexts.Common.Data
{
    // CREDIT: Thanks to Jimmy Bogard for this simple dispatcher method, refactored but used here.
    // CREDIT: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/ 

    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private IDomainEventDispatcher _dispatcher;
        private IIntegrationEventQueue _integrationEvents;
        private IOutboxService _outboxService;
        private IServiceBusSender<IIntegrationEvent> _busSender;

        public AppDbContext(
            DbContextOptions<AppDbContext> options, 
            IDomainEventDispatcher dispatcher, 
            IIntegrationEventQueue integrationEvents,
            IServiceBusSender<IIntegrationEvent> busSender,
            IOutboxService outboxService) : base (options)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _integrationEvents = integrationEvents ?? throw new ArgumentNullException(nameof(integrationEvents));
            _busSender = busSender ?? throw new ArgumentNullException(nameof(busSender));

            _outboxService = outboxService ?? throw new ArgumentNullException(nameof(outboxService));
            _outboxService.AddProvider(this);
        }

        public DbSet<ExamplePerson> ExamplePersons { get; set; }
        public DbSet<OutboxPackage> OutboxPackages { get; set; }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // TODO: What exception handling/unwrapping that needs to take place?

            // TODO: Concerned about single responsibility. This has two:
            // TODO:     1. Persisting data to DB
            // TODO:     2. Publishing events to propagate side-effects
            // TODO: Consider pulling the event dispatching out 
            // TODO: and handling elsewhere.

            var aggregatesWithDomainEvents = ChangeTracker.Entries<IAggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();
            
            await _dispatcher
                .DispatchAndClearDomainEventsAsync(aggregatesWithDomainEvents);
            
            // TODO: Get time span from configuration
            _integrationEvents.CopyToOutbox(_outboxService, TimeSpan.FromDays(7));
            
            var saveResult = 
                await base.SaveChangesAsync(cancellationToken); // Aggregate and outbox changes

            var publishedEvents = await _integrationEvents
                .PublishToBus(_busSender);
            
            // TODO: Add scheduled retries for packages left in outbox
            publishedEvents.RemoveFromOutbox(_outboxService);

            await base.SaveChangesAsync(cancellationToken); // Outbox changes
            
            return saveResult;
        }

        public override int SaveChanges()
        {
            throw new NotImplementedException("Only SaveChangesAsync is implemented in this application.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ExamplePersonTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxPackageTypeConfiguration());
        }
    }
}
