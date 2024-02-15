using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserDataService.Migrations
{
    /// <inheritdoc />
    public partial class Statistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_StatisticsId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatisticsId",
                table: "Users",
                column: "StatisticsId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_StatisticsId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatisticsId",
                table: "Users",
                column: "StatisticsId");
        }
    }
}
