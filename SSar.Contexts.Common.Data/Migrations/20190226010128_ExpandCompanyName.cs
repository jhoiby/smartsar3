using Microsoft.EntityFrameworkCore.Migrations;

namespace SSar.Contexts.Common.Data.Migrations
{
    public partial class ExpandCompanyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_Name",
                table: "MemberOrganizations",
                newName: "Name_ShortName");

            migrationBuilder.AddColumn<string>(
                name: "Name_FullName",
                table: "MemberOrganizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name_ReportingCode",
                table: "MemberOrganizations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_FullName",
                table: "MemberOrganizations");

            migrationBuilder.DropColumn(
                name: "Name_ReportingCode",
                table: "MemberOrganizations");

            migrationBuilder.RenameColumn(
                name: "Name_ShortName",
                table: "MemberOrganizations",
                newName: "Name_Name");
        }
    }
}
