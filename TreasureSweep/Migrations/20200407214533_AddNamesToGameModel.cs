using Microsoft.EntityFrameworkCore.Migrations;

namespace TreasureSweepGame.Migrations
{
    public partial class AddNamesToGameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "P1Name",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "P2Name",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "P1Name",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "P2Name",
                table: "Games");
        }
    }
}
