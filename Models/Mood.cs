using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPlay.API.Models
{
    [Table("Moods")]
    public class Mood
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(50)]
        public string? IconName { get; set; }

        [MaxLength(7)]
        public string? ColorHex { get; set; }

        [Required]
        [MaxLength(10)]
        public string Category { get; set; } = string.Empty; // "sober" or "drunk"

        // Navigation properties
        public virtual ICollection<MoodSong> MoodSongs { get; set; } = new List<MoodSong>();
        public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
    }
}