using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EshopMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class vyrobek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vyrobek",
                columns: table => new
                {
                    VyrobekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nazev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Popis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Obrazek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    KategorieId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vyrobek", x => x.VyrobekId);
                    table.ForeignKey(
                        name: "FK_Vyrobek_Kategorie_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "KategorieId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vyrobek_KategorieId",
                table: "Vyrobek",
                column: "KategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vyrobek");
        }
    }
}
