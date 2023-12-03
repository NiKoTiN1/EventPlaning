using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanning.DataAccess.Migrations
{
    public partial class FixedEventModelTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDescriptions",
                table: "EventModels",
                newName: "EventDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventDescription",
                table: "EventModels",
                newName: "EventDescriptions");
        }
    }
}
