using MoodPlay.API.DTOs;

namespace MoodPlay.API.Services.Interfaces
{
    public interface ISpotifyService
    {
        Task<string> GetAuthorizationUrlAsync(string? redirectUri = null);
        Task<SpotifyTokenResponse?> ExchangeCodeForTokenAsync(string code, string? redirectUri = null);
        Task<SpotifyTokenResponse?> RefreshAccessTokenAsync(string refreshToken);
        Task<SpotifyUserInfo?> GetUserInfoAsync(string accessToken);
        Task<bool> ConnectSpotifyAccountAsync(Guid userId, string spotifyId, string accessToken, string refreshToken);
        Task<bool> DisconnectSpotifyAccountAsync(Guid userId);
    }
}
