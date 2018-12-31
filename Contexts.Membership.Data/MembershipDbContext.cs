using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Membership.Domain.Entities;


namespace SSar.Contexts.Membership.Data
{
    public class MembershipDbContext : DbContext
    {

        public MembershipDbContext(DbContextOptions<MembershipDbContext> options) : base (options)
        {
        }

        public DbSet<ExamplePerson> ExamplePersons { get; set; }

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
