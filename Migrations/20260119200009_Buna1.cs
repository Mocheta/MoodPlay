using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoodPlay.API.Migrations
{
    /// <inheritdoc />
    public partial class Buna1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mood_songs_moods_MoodId",
                table: "mood_songs");

            migrationBuilder.DropForeignKey(
                name: "FK_mood_songs_songs_SongId",
                table: "mood_songs");

            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_moods_MoodId",
                table: "user_sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_users_UserId",
                table: "user_sessions");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "SpotifyId",
                table: "users",
                newName: "spotifyid");

            migrationBuilder.RenameColumn(
                name: "SpotifyAccessToken",
                table: "users",
                newName: "spotifyaccesstoken");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "passwordhash");

            migrationBuilder.RenameColumn(
                name: "LastLogin",
                table: "users",
                newName: "lastlogin");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "users",
                newName: "displayname");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "users",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "AppleMusicId",
                table: "users",
                newName: "applemusicid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_users_Username",
                table: "users",
                newName: "IX_users_username");

            migrationBuilder.RenameIndex(
                name: "IX_users_Email",
                table: "users",
                newName: "IX_users_email");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "user_sessions",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "StartedAt",
                table: "user_sessions",
                newName: "startedat");

            migrationBuilder.RenameColumn(
                name: "MoodId",
                table: "user_sessions",
                newName: "moodid");

            migrationBuilder.RenameColumn(
                name: "Mode",
                table: "user_sessions",
                newName: "mode");

            migrationBuilder.RenameColumn(
                name: "EndedAt",
                table: "user_sessions",
                newName: "endedat");

            migrationBuilder.RenameColumn(
                name: "DrinkLevel",
                table: "user_sessions",
                newName: "drinklevel");

            migrationBuilder.RenameColumn(
                name: "CustomPrompt",
                table: "user_sessions",
                newName: "customprompt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user_sessions",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_UserId",
                table: "user_sessions",
                newName: "IX_user_sessions_userid");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_MoodId",
                table: "user_sessions",
                newName: "IX_user_sessions_moodid");

            migrationBuilder.RenameColumn(
                name: "Valence",
                table: "songs",
                newName: "valence");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "songs",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Tempo",
                table: "songs",
                newName: "tempo");

            migrationBuilder.RenameColumn(
                name: "SpotifyUri",
                table: "songs",
                newName: "spotifyuri");

            migrationBuilder.RenameColumn(
                name: "ReleaseYear",
                table: "songs",
                newName: "releaseyear");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "songs",
                newName: "genre");

            migrationBuilder.RenameColumn(
                name: "EnergyLevel",
                table: "songs",
                newName: "energylevel");

            migrationBuilder.RenameColumn(
                name: "DurationSeconds",
                table: "songs",
                newName: "durationseconds");

            migrationBuilder.RenameColumn(
                name: "Danceability",
                table: "songs",
                newName: "danceability");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "songs",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Artist",
                table: "songs",
                newName: "artist");

            migrationBuilder.RenameColumn(
                name: "AppleMusicId",
                table: "songs",
                newName: "applemusicid");

            migrationBuilder.RenameColumn(
                name: "Album",
                table: "songs",
                newName: "album");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "songs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "moods",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "moods",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "IconName",
                table: "moods",
                newName: "iconname");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "moods",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "moods",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "ColorHex",
                table: "moods",
                newName: "colorhex");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "moods",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "moods",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_moods_Slug",
                table: "moods",
                newName: "IX_moods_slug");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "mood_songs",
                newName: "songid");

            migrationBuilder.RenameColumn(
                name: "RelevanceScore",
                table: "mood_songs",
                newName: "relevancescore");

            migrationBuilder.RenameColumn(
                name: "MoodId",
                table: "mood_songs",
                newName: "moodid");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "mood_songs",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "mood_songs",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_mood_songs_SongId",
                table: "mood_songs",
                newName: "IX_mood_songs_songid");

            migrationBuilder.RenameIndex(
                name: "IX_mood_songs_MoodId_SongId",
                table: "mood_songs",
                newName: "IX_mood_songs_moodid_songid");

            migrationBuilder.AddForeignKey(
                name: "FK_mood_songs_moods_moodid",
                table: "mood_songs",
                column: "moodid",
                principalTable: "moods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mood_songs_songs_songid",
                table: "mood_songs",
                column: "songid",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_moods_moodid",
                table: "user_sessions",
                column: "moodid",
                principalTable: "moods",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_users_userid",
                table: "user_sessions",
                column: "userid",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mood_songs_moods_moodid",
                table: "mood_songs");

            migrationBuilder.DropForeignKey(
                name: "FK_mood_songs_songs_songid",
                table: "mood_songs");

            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_moods_moodid",
                table: "user_sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_user_sessions_users_userid",
                table: "user_sessions");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "spotifyid",
                table: "users",
                newName: "SpotifyId");

            migrationBuilder.RenameColumn(
                name: "spotifyaccesstoken",
                table: "users",
                newName: "SpotifyAccessToken");

            migrationBuilder.RenameColumn(
                name: "passwordhash",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "lastlogin",
                table: "users",
                newName: "LastLogin");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "displayname",
                table: "users",
                newName: "DisplayName");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "applemusicid",
                table: "users",
                newName: "AppleMusicId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "users",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_users_username",
                table: "users",
                newName: "IX_users_Username");

            migrationBuilder.RenameIndex(
                name: "IX_users_email",
                table: "users",
                newName: "IX_users_Email");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "user_sessions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "startedat",
                table: "user_sessions",
                newName: "StartedAt");

            migrationBuilder.RenameColumn(
                name: "moodid",
                table: "user_sessions",
                newName: "MoodId");

            migrationBuilder.RenameColumn(
                name: "mode",
                table: "user_sessions",
                newName: "Mode");

            migrationBuilder.RenameColumn(
                name: "endedat",
                table: "user_sessions",
                newName: "EndedAt");

            migrationBuilder.RenameColumn(
                name: "drinklevel",
                table: "user_sessions",
                newName: "DrinkLevel");

            migrationBuilder.RenameColumn(
                name: "customprompt",
                table: "user_sessions",
                newName: "CustomPrompt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "user_sessions",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_userid",
                table: "user_sessions",
                newName: "IX_user_sessions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_sessions_moodid",
                table: "user_sessions",
                newName: "IX_user_sessions_MoodId");

            migrationBuilder.RenameColumn(
                name: "valence",
                table: "songs",
                newName: "Valence");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "songs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "tempo",
                table: "songs",
                newName: "Tempo");

            migrationBuilder.RenameColumn(
                name: "spotifyuri",
                table: "songs",
                newName: "SpotifyUri");

            migrationBuilder.RenameColumn(
                name: "releaseyear",
                table: "songs",
                newName: "ReleaseYear");

            migrationBuilder.RenameColumn(
                name: "genre",
                table: "songs",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "energylevel",
                table: "songs",
                newName: "EnergyLevel");

            migrationBuilder.RenameColumn(
                name: "durationseconds",
                table: "songs",
                newName: "DurationSeconds");

            migrationBuilder.RenameColumn(
                name: "danceability",
                table: "songs",
                newName: "Danceability");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "songs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "artist",
                table: "songs",
                newName: "Artist");

            migrationBuilder.RenameColumn(
                name: "applemusicid",
                table: "songs",
                newName: "AppleMusicId");

            migrationBuilder.RenameColumn(
                name: "album",
                table: "songs",
                newName: "Album");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "songs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "moods",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "moods",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "iconname",
                table: "moods",
                newName: "IconName");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "moods",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "moods",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "colorhex",
                table: "moods",
                newName: "ColorHex");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "moods",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "moods",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_moods_slug",
                table: "moods",
                newName: "IX_moods_Slug");

            migrationBuilder.RenameColumn(
                name: "songid",
                table: "mood_songs",
                newName: "SongId");

            migrationBuilder.RenameColumn(
                name: "relevancescore",
                table: "mood_songs",
                newName: "RelevanceScore");

            migrationBuilder.RenameColumn(
                name: "moodid",
                table: "mood_songs",
                newName: "MoodId");

            migrationBuilder.RenameColumn(
                name: "createdat",
                table: "mood_songs",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "mood_songs",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_mood_songs_songid",
                table: "mood_songs",
                newName: "IX_mood_songs_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_mood_songs_moodid_songid",
                table: "mood_songs",
                newName: "IX_mood_songs_MoodId_SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_mood_songs_moods_MoodId",
                table: "mood_songs",
                column: "MoodId",
                principalTable: "moods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_mood_songs_songs_SongId",
                table: "mood_songs",
                column: "SongId",
                principalTable: "songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_moods_MoodId",
                table: "user_sessions",
                column: "MoodId",
                principalTable: "moods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_user_sessions_users_UserId",
                table: "user_sessions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
