namespace MoodPlay.API.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? SpotifyId { get; set; }
        public string? Token { get; set; } // JWT token for login response
    }
}