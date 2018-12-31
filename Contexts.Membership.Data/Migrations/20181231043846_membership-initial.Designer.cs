﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SSar.Contexts.Membership.Data;

namespace SSar.Contexts.Membership.Data.Migrations
{
    [DbContext(typeof(MembershipDbContext))]
    [Migration("20181231043846_membership-initial")]
    partial class membershipinitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SSar.Contexts.Membership.Domain.Entities.ExamplePerson", b =>
                {
                    b.Property<Guid>("_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Name");

                    b.HasKey("_id");

                    b.ToTable("ExamplePersons");
                });
#pragma warning restore 612, 618
        }
    }
}
