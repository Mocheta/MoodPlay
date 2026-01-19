using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;
using System.Security.Claims;

namespace MoodPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto? registerDto)
        {
            if (registerDto == null)
            {
                return BadRequest(new { message = "Request body is required. Please provide email, username, and password." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors.Select(e => new { Field = x.Key, Error = e.ErrorMessage }))
                    .ToList();
                
                return BadRequest(new { 
                    message = "Validation failed", 
                    errors = errors 
                });
            }

            var user = await _authService.RegisterAsync(registerDto);

            if (user == null)
            {
                return Conflict(new { message = "User with this email or username already exists" });
            }

            return Ok(user);
        }

        /// <summary>
        /// Login with email/username and password
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto? loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest(new { message = "Request body is required. Please provide emailOrUsername and password." });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .SelectMany(x => x.Value!.Errors.Select(e => new { Field = x.Key, Error = e.ErrorMessage }))
                    .ToList();
                
                return BadRequest(new { 
                    message = "Validation failed", 
                    errors = errors 
                });
            }

            var user = await _authService.LoginAsync(loginDto);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid email/username or password" });
            }

            return Ok(user);
        }

        /// <summary>
        /// Get current user information from JWT token
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var userDto = await _authService.GetUserDtoByIdAsync(userId);
            if (userDto == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(userDto);
        }
    }
}
