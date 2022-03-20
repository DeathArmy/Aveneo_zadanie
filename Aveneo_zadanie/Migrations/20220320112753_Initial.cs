using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aveneo_zadanie.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    currency1 = table.Column<string>(type: "VARCHAR(5)", nullable: false),
                    currency2 = table.Column<string>(type: "VARCHAR(5)", nullable: false),
                    dataDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    exchangeRate = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => new { x.currency1, x.currency2, x.dataDate });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");
        }
    }
}
