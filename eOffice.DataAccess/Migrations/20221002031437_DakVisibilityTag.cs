using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eOffice.DataAccess.Migrations
{
    public partial class DakVisibilityTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DakVisibilityTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DakId = table.Column<int>(type: "int", nullable: false),
                    Cdr = table.Column<bool>(type: "bit", nullable: false),
                    DyCdr = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DakVisibilityTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DakVisibilityTag_Dak_DakId",
                        column: x => x.DakId,
                        principalTable: "Dak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DakVisibilityTag_DakId",
                table: "DakVisibilityTag",
                column: "DakId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DakVisibilityTag");
        }
    }
}
