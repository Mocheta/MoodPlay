using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;

namespace MoodPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SongController : ControllerBase
    {
        private readonly ISongService _songService;
        private readonly ILogger<SongController> _logger;

        public SongController(ISongService songService, ILogger<SongController> logger)
        {
            _songService = songService;
            _logger = logger;
        }

        /// <summary>
        /// Get songs by mood ID
        /// </summary>
        [HttpGet("mood/{moodId}")]
        public async Task<ActionResult<List<SongDto>>> GetSongsByMood(Guid moodId)
        {
            var songs = await _songService.GetSongsByMoodAsync(moodId);
            return Ok(songs);
        }

        /// <summary>
        /// Get songs by session ID
        /// </summary>
        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<List<SongDto>>> GetSongsBySession(Guid sessionId)
        {
            var songs = await _songService.GetSongsBySessionAsync(sessionId);
            return Ok(songs);
        }

        /// <summary>
        /// Get a specific song by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<SongDto>> GetSongById(Guid id)
        {
            var song = await _songService.GetSongByIdAsync(id);

            if (song == null)
            {
                return NotFound(new { message = "Song not found" });
            }

            return Ok(song);
        }

        /// <summary>
        /// Search songs by title, artist, or album
        /// </summary>
        [HttpGet("search")]
        public async Task<ActionResult<List<SongDto>>> SearchSongs([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest(new { message = "Search query is required" });
            }

            var songs = await _songService.SearchSongsAsync(q);
            return Ok(songs);
        }
    }
}
