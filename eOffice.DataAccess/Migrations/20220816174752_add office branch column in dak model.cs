using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eOffice.DataAccess.Migrations
{
    public partial class addofficebranchcolumnindakmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficeBranch",
                table: "Dak",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfficeBranch",
                table: "Dak");
        }
    }
}
