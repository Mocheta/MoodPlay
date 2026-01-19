using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodPlay.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCreatedAtColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdat",
                table: "users");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "songs");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "moods");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "mood_songs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "songs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "moods",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "mood_songs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
