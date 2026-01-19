using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodPlay.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnnecessaryUserColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "displayname",
                table: "users");

            migrationBuilder.DropColumn(
                name: "lastlogin",
                table: "users");

            migrationBuilder.DropColumn(
                name: "spotifyid",
                table: "users");

            migrationBuilder.DropColumn(
                name: "spotifyaccesstoken",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "displayname",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "lastlogin",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "spotifyid",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "spotifyaccesstoken",
                table: "users",
                type: "text",
                nullable: true);
        }
    }
}
