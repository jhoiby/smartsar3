using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SSar.Contexts.Common.Entities;
using SSar.Contexts.Membership.Domain.Entities;
using MediatR;

namespace SSar.Contexts.Membership.Data
{
    public class MembershipDbContext : DbContext
    {
        private IMediator _dispatcher;

        public MembershipDbContext(DbContextOptions<MembershipDbContext> options, IMediator dispatcher) : base (options)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException();
        }

        public DbSet<ExamplePerson> ExamplePersons { get; set; }
        
        public override int SaveChanges()
        {
            throw new NotImplementedException("Only SaveChangesAsync is implemented in this application.");
        }

        // TODO: DEEP STUDYING ON UNWRAPPING ASYNC EXCEPTIONS!!!
        // TODO: Testing of this method and its side effects, and exceptions at various points
        // CREDIT: Thanks to Jimmy Bogard for this simple dispatcher method.
        // CREDIT: https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/ 
        //
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var domainEventEntities = ChangeTracker.Entries<IAggregateRoot>()
                .Select(po => po.Entity)
                .Where(po => po.Events.Any())
                .ToArray();

            foreach (var entity in domainEventEntities)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await _dispatcher.Publish(domainEvent);
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamplePerson>()
                .HasKey("_id");
            modelBuilder.Entity<ExamplePerson>()
                .Property(b => b.Name)
                .HasField("_name");
            modelBuilder.Entity<ExamplePerson>()
                .Property(b => b.EmailAddress)
                .HasField("_emailAddress");
        }
    }
}
