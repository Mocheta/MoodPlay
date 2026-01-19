using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPlay.API.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? DisplayName { get; set; }

        public DateTime? LastLogin { get; set; }

        [MaxLength(100)]
        public string? SpotifyId { get; set; }

        public string? SpotifyAccessToken { get; set; }

        // Navigation properties
        public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
    }
}