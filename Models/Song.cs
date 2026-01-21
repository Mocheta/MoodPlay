using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPlay.API.Models
{
    [Table("songs")]
    public class Song
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Artist { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Album { get; set; }

        public int? DurationSeconds { get; set; }

        [MaxLength(100)]
        public string? SpotifyUri { get; set; }

        public int? ReleaseYear { get; set; }

        [MaxLength(100)]
        public string? Genre { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? EnergyLevel { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? Valence { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? Danceability { get; set; }

        public int? Tempo { get; set; }

        // Navigation properties
        public virtual ICollection<MoodSong> MoodSongs { get; set; } = new List<MoodSong>();
    }
}
