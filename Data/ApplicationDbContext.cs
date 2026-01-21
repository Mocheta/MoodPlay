using Microsoft.EntityFrameworkCore;
using MoodPlay.API.Models;

namespace MoodPlay.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
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

            // User indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Mood indexes
            modelBuilder.Entity<Mood>()
                .HasIndex(m => m.Slug)
                .IsUnique();

            // MoodSong unique constraint
            modelBuilder.Entity<MoodSong>()
                .HasIndex(ms => new { ms.MoodId, ms.SongId })
                .IsUnique();

            // Relationships
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
