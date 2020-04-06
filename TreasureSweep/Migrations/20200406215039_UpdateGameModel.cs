using Microsoft.EntityFrameworkCore.Migrations;

namespace TreasureSweepGame.Migrations
{
    public partial class UpdateGameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WinnerId",
                table: "Games",
                newName: "WinningPlayer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WinningPlayer",
                table: "Games",
                newName: "WinnerId");
        }
    }
}
