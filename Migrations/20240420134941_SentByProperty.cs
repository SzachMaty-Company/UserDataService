using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserDataService.Migrations
{
    /// <inheritdoc />
    public partial class SentByProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SentBy",
                table: "Friendships",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SentBy",
                table: "Friendships");
        }
    }
}
