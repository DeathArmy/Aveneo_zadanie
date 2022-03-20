using Microsoft.EntityFrameworkCore.Migrations;

namespace Aveneo_zadanie.Migrations
{
    public partial class ApiKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratedKeys",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    key = table.Column<string>(type: "VARCHAR(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedKeys", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedKeys");
        }
    }
}
