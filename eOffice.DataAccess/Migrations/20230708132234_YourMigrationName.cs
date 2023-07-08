using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace eOffice.DataAccess.Migrations
{
    public partial class YourMigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DakSpeak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DakId = table.Column<int>(type: "integer", nullable: false),
                    MarkedForId = table.Column<string>(type: "text", nullable: false),
                    MarkedById = table.Column<string>(type: "text", nullable: false),
                    MarkedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DakSpeak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DakSpeak_AspNetUsers_MarkedById",
                        column: x => x.MarkedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DakSpeak_AspNetUsers_MarkedForId",
                        column: x => x.MarkedForId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DakSpeak_Dak_DakId",
                        column: x => x.DakId,
                        principalTable: "Dak",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DakSpeak_DakId",
                table: "DakSpeak",
                column: "DakId");

            migrationBuilder.CreateIndex(
                name: "IX_DakSpeak_MarkedById",
                table: "DakSpeak",
                column: "MarkedById");

            migrationBuilder.CreateIndex(
                name: "IX_DakSpeak_MarkedForId",
                table: "DakSpeak",
                column: "MarkedForId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DakSpeak");
        }
    }
}
