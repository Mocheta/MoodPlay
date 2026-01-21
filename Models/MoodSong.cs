using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPlay.API.Models
{
    [Table("mood_songs")]
    public class MoodSong
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid MoodId { get; set; }

        [Required]
        public Guid SongId { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        public decimal? RelevanceScore { get; set; }

        // Navigation properties
        [ForeignKey("MoodId")]
        public virtual Mood Mood { get; set; } = null!;

        [ForeignKey("SongId")]
        public virtual Song Song { get; set; } = null!;
    }
}
