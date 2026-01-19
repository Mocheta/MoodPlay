using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodPlay.API.Migrations
{
    /// <inheritdoc />
    public partial class Buna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "moods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IconName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ColorHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: true),
                    Category = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Artist = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Album = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DurationSeconds = table.Column<int>(type: "integer", nullable: true),
                    SpotifyUri = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AppleMusicId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ReleaseYear = table.Column<int>(type: "integer", nullable: true),
                    Genre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EnergyLevel = table.Column<decimal>(type: "numeric(3,2)", nullable: true),
                    Valence = table.Column<decimal>(type: "numeric(3,2)", nullable: true),
                    Danceability = table.Column<decimal>(type: "numeric(3,2)", nullable: true),
                    Tempo = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_songs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SpotifyId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SpotifyAccessToken = table.Column<string>(type: "text", nullable: true),
                    AppleMusicId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mood_songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MoodId = table.Column<Guid>(type: "uuid", nullable: false),
                    SongId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelevanceScore = table.Column<decimal>(type: "numeric(3,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mood_songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mood_songs_moods_MoodId",
                        column: x => x.MoodId,
                        principalTable: "moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mood_songs_songs_SongId",
                        column: x => x.SongId,
                        principalTable: "songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Mode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MoodId = table.Column<Guid>(type: "uuid", nullable: true),
                    CustomPrompt = table.Column<string>(type: "text", nullable: true),
                    DrinkLevel = table.Column<int>(type: "integer", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_sessions_moods_MoodId",
                        column: x => x.MoodId,
                        principalTable: "moods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_user_sessions_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mood_songs_MoodId_SongId",
                table: "mood_songs",
                columns: new[] { "MoodId", "SongId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mood_songs_SongId",
                table: "mood_songs",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_moods_Slug",
                table: "moods",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_sessions_MoodId",
                table: "user_sessions",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_user_sessions_UserId",
                table: "user_sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mood_songs");

            migrationBuilder.DropTable(
                name: "user_sessions");

            migrationBuilder.DropTable(
                name: "songs");

            migrationBuilder.DropTable(
                name: "moods");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
