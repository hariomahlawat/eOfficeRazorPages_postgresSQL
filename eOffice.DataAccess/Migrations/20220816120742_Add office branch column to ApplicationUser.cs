using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eOffice.DataAccess.Migrations
{
    public partial class AddofficebranchcolumntoApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficeBranch",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeBranch",
                table: "AspNetUsers");
        }
    }
}
