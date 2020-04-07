using Microsoft.EntityFrameworkCore.Migrations;

namespace TreasureSweepGame.Migrations
{
    public partial class AddCurrentUserProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayer",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPlayer",
                table: "Games");
        }
    }
}
