using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SSar.Contexts.Common.Entities;
using MediatR;
using SSar.Contexts.Common.Events;
using SSar.Contexts.Membership.Data.TypeConfigurations;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson;

namespace SSar.Contexts.Membership.Data
{
    // CREDIT: Thanks to Jimmy Bogard for this simple dispatcher method, refactored but used here.
    // CREDIT: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/ 

    public class MembershipDbContext : DbContext
    {
        private IEventDispatcher _dispatcher;

        public MembershipDbContext(DbContextOptions<MembershipDbContext> options, IEventDispatcher dispatcher) : base (options)
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
                .DispatchInternalBoundedContextEventsAsync(aggregatesWithDomainEvents);

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
