using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSar.Domain.IdentityAuth.Entities;
using SSar.Data.TypeConfigurations;
using SSar.Domain.Membership.ExamplePersons;
using SSar.Infrastructure.DomainEvents;
using SSar.Infrastructure.Entities;

namespace SSar.Data
{
    // CREDIT: Thanks to Jimmy Bogard for this simple dispatcher method, refactored but used here.
    // CREDIT: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/ 

    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private IDomainEventDispatcher _dispatcher;

        public AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher dispatcher) : base (options)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        public DbSet<ExamplePerson> ExamplePersons { get; set; }
        
        public override int SaveChanges()
        {
            throw new NotImplementedException("Only SaveChangesAsync is implemented in this application.");
        }

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
                .DispatchDomainEventsAsync(aggregatesWithDomainEvents);

            _dispatcher
                .ClearEventEntities(aggregatesWithDomainEvents);

            // Transaction closing includes side-effects of dispatched events
            var saveResult = 
                await base.SaveChangesAsync(cancellationToken);
            
            return saveResult;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ExamplePersonTypeConfiguration());
        }
    }
}
