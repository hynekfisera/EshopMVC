using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EshopMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Kategorie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    KategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentKategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.KategorieId);
                    table.ForeignKey(
                        name: "FK_Kategorie_Kategorie_ParentKategorieId",
                        column: x => x.ParentKategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "KategorieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kategorie_ParentKategorieId",
                table: "Kategorie",
                column: "ParentKategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kategorie");
        }
    }
}
