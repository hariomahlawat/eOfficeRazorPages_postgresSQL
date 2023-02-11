using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eOffice.DataAccess.Migrations
{
    public partial class EventCalendarcolumnnamechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "EventCalendar",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EventCalendar",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "EventCalendar",
                newName: "End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "EventCalendar",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "EventCalendar",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "EventCalendar",
                newName: "EndDate");
        }
    }
}
