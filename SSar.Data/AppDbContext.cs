using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSar.Data.Outbox;
using SSar.Domain.IdentityAuth.Entities;
using SSar.Data.TypeConfigurations;
using SSar.Domain.Membership.ExamplePersons;
using SSar.Infrastructure.DomainEvents;
using SSar.Infrastructure.Entities;
using SSar.Infrastructure.IntegrationEvents;
using SSar.Infrastructure.ServiceBus;

namespace SSar.Data
{
    // CREDIT: Thanks to Jimmy Bogard for this simple dispatcher method, refactored but used here.
    // CREDIT: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/ 

    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private IDomainEventDispatcher _dispatcher;
        private IIntegrationEventQueue _integrationEvents;
        private IOutboxService _outboxService;
        private IServiceBusSender _busSender;

        public AppDbContext(
            DbContextOptions<AppDbContext> options, 
            IDomainEventDispatcher dispatcher, 
            IIntegrationEventQueue integrationEvents,
            IServiceBusSender busSender) : base (options)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _integrationEvents = integrationEvents ?? throw new ArgumentNullException(nameof(integrationEvents));
            _busSender = busSender ?? throw new ArgumentNullException(nameof(busSender));

            _outboxService = new OutboxService(this); // ?? throw new ArgumentNullException(nameof(outboxService));
        }

        public DbSet<ExamplePerson> ExamplePersons { get; set; }
        public DbSet<OutboxPackage> OutboxPackages { get; set; }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // TODO: What exception handling/unwrapping that needs to take place?

            // TODO: Concerned about single responsibility. Consider pulling the
            // TODO: event dispatching out and handling elsewhere.

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
