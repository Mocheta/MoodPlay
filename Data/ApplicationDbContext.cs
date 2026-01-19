using Microsoft.EntityFrameworkCore;
using MoodPlay.API.Models;

namespace MoodPlay.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Mood> Moods { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<MoodSong> MoodSongs { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names to match PostgreSQL (lowercase)
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Mood>().ToTable("moods");
            modelBuilder.Entity<Song>().ToTable("songs");
            modelBuilder.Entity<MoodSong>().ToTable("mood_songs");
            modelBuilder.Entity<UserSession>().ToTable("user_sessions");

            // Configure User column mappings (PostgreSQL uses snake_case or lowercase)
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Username).HasColumnName("username");
                entity.Property(e => e.PasswordHash).HasColumnName("passwordhash");
                entity.Property(e => e.DisplayName).HasColumnName("displayname");
                entity.Property(e => e.LastLogin).HasColumnName("lastlogin");
                entity.Property(e => e.SpotifyId).HasColumnName("spotifyid");
                entity.Property(e => e.SpotifyAccessToken).HasColumnName("spotifyaccesstoken");
            });

            // Configure Mood column mappings
            modelBuilder.Entity<Mood>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Slug).HasColumnName("slug");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.IconName).HasColumnName("iconname");
                entity.Property(e => e.ColorHex).HasColumnName("colorhex");
                entity.Property(e => e.Category).HasColumnName("category");
            });

            // Configure Song column mappings
            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Artist).HasColumnName("artist");
                entity.Property(e => e.Album).HasColumnName("album");
                entity.Property(e => e.DurationSeconds).HasColumnName("durationseconds");
                entity.Property(e => e.SpotifyUri).HasColumnName("spotifyuri");
                entity.Property(e => e.ReleaseYear).HasColumnName("releaseyear");
                entity.Property(e => e.Genre).HasColumnName("genre");
                entity.Property(e => e.EnergyLevel).HasColumnName("energylevel");
                entity.Property(e => e.Valence).HasColumnName("valence");
                entity.Property(e => e.Danceability).HasColumnName("danceability");
                entity.Property(e => e.Tempo).HasColumnName("tempo");
            });

            // Configure MoodSong column mappings
            modelBuilder.Entity<MoodSong>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.MoodId).HasColumnName("moodid");
                entity.Property(e => e.SongId).HasColumnName("songid");
                entity.Property(e => e.RelevanceScore).HasColumnName("relevancescore");
            });

            // Configure UserSession column mappings
            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("userid");
                entity.Property(e => e.Mode).HasColumnName("mode");
                entity.Property(e => e.MoodId).HasColumnName("moodid");
                entity.Property(e => e.CustomPrompt).HasColumnName("customprompt");
                entity.Property(e => e.DrinkLevel).HasColumnName("drinklevel");
                entity.Property(e => e.StartedAt).HasColumnName("startedat");
                entity.Property(e => e.EndedAt).HasColumnName("endedat");
            });

            // Configure unique constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Mood>()
                .HasIndex(m => m.Slug)
                .IsUnique();

            // Configure MoodSong unique constraint
            modelBuilder.Entity<MoodSong>()
                .HasIndex(ms => new { ms.MoodId, ms.SongId })
                .IsUnique();

            // Configure relationships
            modelBuilder.Entity<MoodSong>()
                .HasOne(ms => ms.Mood)
                .WithMany(m => m.MoodSongs)
                .HasForeignKey(ms => ms.MoodId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MoodSong>()
                .HasOne(ms => ms.Song)
                .WithMany(s => s.MoodSongs)
                .HasForeignKey(ms => ms.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSession>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSessions)
                .HasForeignKey(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserSession>()
                .HasOne(us => us.Mood)
                .WithMany(m => m.UserSessions)
                .HasForeignKey(us => us.MoodId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}