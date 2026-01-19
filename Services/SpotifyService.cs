using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoodPlay.API.Data;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace MoodPlay.API.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<SpotifyService> _logger;

        private string ClientId => _configuration["Spotify:ClientId"] ?? throw new InvalidOperationException("Spotify ClientId not configured");
        private string ClientSecret => _configuration["Spotify:ClientSecret"] ?? throw new InvalidOperationException("Spotify ClientSecret not configured");
        private string RedirectUri => _configuration["Spotify:RedirectUri"] ?? "http://localhost:7169/api/spotify/callback";

        public SpotifyService(
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            ILogger<SpotifyService> logger)
        {
            _context = context;
            _configuration = configuration;
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public Task<string> GetAuthorizationUrlAsync(string? redirectUri = null)
        {
            var redirect = redirectUri ?? RedirectUri;
            var scopes = "user-read-email user-read-private user-read-playback-state user-modify-playback-state user-read-currently-playing playlist-read-private playlist-read-collaborative playlist-modify-public playlist-modify-private";
            var state = Guid.NewGuid().ToString();

            var authUrl = $"https://accounts.spotify.com/authorize?" +
                $"client_id={ClientId}" +
                $"&response_type=code" +
                $"&redirect_uri={Uri.EscapeDataString(redirect)}" +
                $"&scope={Uri.EscapeDataString(scopes)}" +
                $"&state={state}";

            return Task.FromResult(authUrl);
        }

        public async Task<SpotifyTokenResponse?> ExchangeCodeForTokenAsync(string code, string? redirectUri = null)
        {
            try
            {
                var redirect = redirectUri ?? RedirectUri;
                var tokenUrl = "https://accounts.spotify.com/api/token";

                var requestBody = new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "code", code },
                    { "redirect_uri", redirect },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
                {
                    Content = new FormUrlEncodedContent(requestBody)
                };

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return tokenResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error exchanging Spotify code for token");
                return null;
            }
        }

        public async Task<SpotifyTokenResponse?> RefreshAccessTokenAsync(string refreshToken)
        {
            try
            {
                var tokenUrl = "https://accounts.spotify.com/api/token";

                var requestBody = new Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", refreshToken },
                    { "client_id", ClientId },
                    { "client_secret", ClientSecret }
                };

                var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
                {
                    Content = new FormUrlEncodedContent(requestBody)
                };

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<SpotifyTokenResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return tokenResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing Spotify access token");
                return null;
            }
        }

        public async Task<SpotifyUserInfo?> GetUserInfoAsync(string accessToken)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.spotify.com/v1/me");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<SpotifyUserInfo>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return userInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting Spotify user info");
                return null;
            }
        }

        public async Task<bool> ConnectSpotifyAccountAsync(Guid userId, string spotifyId, string accessToken, string refreshToken)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                user.SpotifyId = spotifyId;
                user.SpotifyAccessToken = accessToken;
                // Note: Refresh token should be stored securely, consider encrypting it
                // For now, storing in the same field (you might want a separate field)
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error connecting Spotify account");
                return false;
            }
        }

        public async Task<bool> DisconnectSpotifyAccountAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                user.SpotifyId = null;
                user.SpotifyAccessToken = null;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disconnecting Spotify account");
                return false;
            }
        }
    }
}
