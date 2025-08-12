using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songdle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSpotifyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpotifyId",
                table: "Songs",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpotifyId",
                table: "Songs");
        }
    }
}
