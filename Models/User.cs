using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoodPlay.API.Models
{
    [Table("users")]
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

        // Spotify integration
        [MaxLength(100)]
        public string? SpotifyId { get; set; }

        public string? SpotifyAccessToken { get; set; }

        public string? SpotifyRefreshToken { get; set; }

        // Navigation properties
        public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();
    }
}
