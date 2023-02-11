using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eOffice.DataAccess.Migrations
{
    public partial class AddMarkedDaktodatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarkedDak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DakId = table.Column<int>(type: "int", nullable: false),
                    OfficersToSee = table.Column<bool>(type: "bit", nullable: false),
                    SainikSammelan = table.Column<bool>(type: "bit", nullable: false),
                    RollCall = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkedDak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarkedDak_Dak_DakId",
                        column: x => x.DakId,
                        principalTable: "Dak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarkedDak_DakId",
                table: "MarkedDak",
                column: "DakId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarkedDak");
        }
    }
}
