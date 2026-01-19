namespace MoodPlay.API.DTOs
{
    public class SpotifyAuthDto
    {
        public string Code { get; set; } = string.Empty;
        public string? RedirectUri { get; set; }
    }

    public class SpotifyTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; } = "Bearer";
        public string? Scope { get; set; }
    }

    public class SpotifyUserInfo
    {
        public string Id { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? Product { get; set; } // premium, free, etc.
    }
}
