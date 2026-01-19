using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;
using System.Security.Claims;

namespace MoodPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ISessionService sessionService, ILogger<SessionController> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("Invalid user token");
        }

        /// <summary>
        /// Create a new session
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<SessionDto>> CreateSession([FromBody] CreateSessionDto createSessionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (createSessionDto.Mode.ToLower() != "sober" && createSessionDto.Mode.ToLower() != "drunk")
            {
                return BadRequest(new { message = "Mode must be 'sober' or 'drunk'" });
            }

            var userId = GetCurrentUserId();
            var session = await _sessionService.CreateSessionAsync(userId, createSessionDto);

            if (session == null)
            {
                return BadRequest(new { message = "Failed to create session" });
            }

            return CreatedAtAction(nameof(GetSessionById), new { id = session.Id }, session);
        }

        /// <summary>
        /// Get a specific session by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSessionById(Guid id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);

            if (session == null)
            {
                return NotFound(new { message = "Session not found" });
            }

            var userId = GetCurrentUserId();
            if (session.UserId != userId)
            {
                return Forbid();
            }

            return Ok(session);
        }

        /// <summary>
        /// Get all sessions for the current user
        /// </summary>
        [HttpGet("my-sessions")]
        public async Task<ActionResult<List<SessionDto>>> GetMySessions()
        {
            var userId = GetCurrentUserId();
            var sessions = await _sessionService.GetUserSessionsAsync(userId);
            return Ok(sessions);
        }

        /// <summary>
        /// End a session
        /// </summary>
        [HttpPost("{id}/end")]
        public async Task<ActionResult> EndSession(Guid id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);

            if (session == null)
            {
                return NotFound(new { message = "Session not found" });
            }

            var userId = GetCurrentUserId();
            if (session.UserId != userId)
            {
                return Forbid();
            }

            var success = await _sessionService.EndSessionAsync(id);

            if (!success)
            {
                return BadRequest(new { message = "Failed to end session" });
            }

            return Ok(new { message = "Session ended successfully" });
        }
    }
}
