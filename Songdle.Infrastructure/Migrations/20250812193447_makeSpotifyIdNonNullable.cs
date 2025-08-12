using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songdle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class makeSpotifyIdNonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Songs_SongOfTheDayId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_SongOfTheDayId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "SongOfTheDayId",
                table: "Games");

            migrationBuilder.UpdateData(
                table: "Songs",
                keyColumn: "SpotifyId",
                keyValue: null,
                column: "SpotifyId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SpotifyId",
                table: "Songs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SpotifyId",
                table: "Songs",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SongOfTheDayId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_SongOfTheDayId",
                table: "Games",
                column: "SongOfTheDayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Songs_SongOfTheDayId",
                table: "Games",
                column: "SongOfTheDayId",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
