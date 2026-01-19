using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;
using System.Security.Claims;

namespace MoodPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IAuthService _authService;
        private readonly ILogger<SpotifyController> _logger;

        public SpotifyController(
            ISpotifyService spotifyService,
            IAuthService authService,
            ILogger<SpotifyController> logger)
        {
            _spotifyService = spotifyService;
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Get Spotify authorization URL to start OAuth flow
        /// </summary>
        [HttpGet("auth-url")]
        [Authorize]
        public async Task<ActionResult<object>> GetAuthUrl([FromQuery] string? redirectUri = null)
        {
            var authUrl = await _spotifyService.GetAuthorizationUrlAsync(redirectUri);
            return Ok(new { authUrl });
        }

        /// <summary>
        /// Handle Spotify OAuth callback and connect account
        /// </summary>
        [HttpPost("connect")]
        [Authorize]
        public async Task<ActionResult> ConnectSpotify([FromBody] SpotifyAuthDto authDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            // Exchange code for tokens
            var tokenResponse = await _spotifyService.ExchangeCodeForTokenAsync(authDto.Code, authDto.RedirectUri);
            if (tokenResponse == null)
            {
                return BadRequest(new { message = "Failed to exchange code for token" });
            }

            // Get user info from Spotify
            var userInfo = await _spotifyService.GetUserInfoAsync(tokenResponse.AccessToken);
            if (userInfo == null)
            {
                return BadRequest(new { message = "Failed to get user info from Spotify" });
            }

            // Connect the account
            var success = await _spotifyService.ConnectSpotifyAccountAsync(
                userId,
                userInfo.Id,
                tokenResponse.AccessToken,
                tokenResponse.RefreshToken);

            if (!success)
            {
                return BadRequest(new { message = "Failed to connect Spotify account" });
            }

            return Ok(new { message = "Spotify account connected successfully", spotifyId = userInfo.Id });
        }

        /// <summary>
        /// Disconnect Spotify account
        /// </summary>
        [HttpPost("disconnect")]
        [Authorize]
        public async Task<ActionResult> DisconnectSpotify()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var success = await _spotifyService.DisconnectSpotifyAccountAsync(userId);
            if (!success)
            {
                return BadRequest(new { message = "Failed to disconnect Spotify account" });
            }

            return Ok(new { message = "Spotify account disconnected successfully" });
        }

        /// <summary>
        /// Refresh Spotify access token
        /// </summary>
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult<SpotifyTokenResponse>> RefreshToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            // Note: You'll need to store refresh token separately to use this endpoint
            // For now, this is a placeholder
            return BadRequest(new { message = "Refresh token functionality requires storing refresh token separately" });
        }
    }
}
