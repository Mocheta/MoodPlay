using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodPlay.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAppleMusicIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "applemusicid",
                table: "users");

            migrationBuilder.DropColumn(
                name: "applemusicid",
                table: "songs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "applemusicid",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "applemusicid",
                table: "songs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
