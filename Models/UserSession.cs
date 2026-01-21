using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPlay.API.Models
{
    [Table("user_sessions")]
    public class UserSession
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Mode { get; set; } = "sober";

        public Guid? MoodId { get; set; }

        public string? CustomPrompt { get; set; }

        public int? DrinkLevel { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? EndedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        [ForeignKey("MoodId")]
        public virtual Mood? Mood { get; set; }
    }
}
