using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EshopMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class sleva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SlevaId",
                table: "Vyrobek",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sleva",
                columns: table => new
                {
                    SlevaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hodnota = table.Column<double>(type: "float", nullable: false),
                    JeProcentualni = table.Column<bool>(type: "bit", nullable: false),
                    MinimalniPocet = table.Column<int>(type: "int", nullable: false),
                    DatumZacatku = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumKonce = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sleva", x => x.SlevaId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vyrobek_SlevaId",
                table: "Vyrobek",
                column: "SlevaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vyrobek_Sleva_SlevaId",
                table: "Vyrobek",
                column: "SlevaId",
                principalTable: "Sleva",
                principalColumn: "SlevaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vyrobek_Sleva_SlevaId",
                table: "Vyrobek");

            migrationBuilder.DropTable(
                name: "Sleva");

            migrationBuilder.DropIndex(
                name: "IX_Vyrobek_SlevaId",
                table: "Vyrobek");

            migrationBuilder.DropColumn(
                name: "SlevaId",
                table: "Vyrobek");
        }
    }
}
