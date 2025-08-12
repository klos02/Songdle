using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songdle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSpotifySongIdToTodaysGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpotifySongId",
                table: "Games",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotifySongId",
                table: "Games");
        }
    }
}
