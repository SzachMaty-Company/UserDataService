using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserDataService.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WinrateIA",
                table: "Statistics",
                newName: "WinrateAI");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Games",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Win",
                table: "Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "WinrateAgainst",
                table: "Friendships",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_BlackId",
                table: "Games",
                column: "BlackId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WhiteId",
                table: "Games",
                column: "WhiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_BlackId",
                table: "Games",
                column: "BlackId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Users_WhiteId",
                table: "Games",
                column: "WhiteId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_BlackId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Users_WhiteId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_BlackId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_WhiteId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Win",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "WinrateAgainst",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "WinrateAI",
                table: "Statistics",
                newName: "WinrateIA");
        }
    }
}
