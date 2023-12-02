using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventPlanning.DataAccess.Migrations
{
    public partial class ChangedEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventDescriptions",
                table: "EventModels",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "EventModels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDescriptions",
                table: "EventModels");

            migrationBuilder.DropColumn(
                name: "EventName",
                table: "EventModels");
        }
    }
}
