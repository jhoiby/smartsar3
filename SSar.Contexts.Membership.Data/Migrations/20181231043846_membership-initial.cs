using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SSar.Contexts.Membership.Data.Migrations
{
    public partial class membershipinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamplePersons",
                columns: table => new
                {
                    _id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamplePersons", x => x._id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamplePersons");
        }
    }
}
