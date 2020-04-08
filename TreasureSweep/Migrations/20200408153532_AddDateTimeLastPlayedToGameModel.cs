using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TreasureSweepGame.Migrations
{
    public partial class AddDateTimeLastPlayedToGameModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastPlayed",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPlayed",
                table: "Games");
        }
    }
}
