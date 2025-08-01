using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Songdle.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Songs_SongOfTheDayId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "SongOfTheDayId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Songs_SongOfTheDayId",
                table: "Games",
                column: "SongOfTheDayId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Songs_SongOfTheDayId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "SongOfTheDayId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Songs_SongOfTheDayId",
                table: "Games",
                column: "SongOfTheDayId",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
