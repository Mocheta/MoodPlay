using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;

namespace MoodPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodController : ControllerBase
    {
        private readonly IMoodService _moodService;
        private readonly ILogger<MoodController> _logger;

        public MoodController(IMoodService moodService, ILogger<MoodController> logger)
        {
            _moodService = moodService;
            _logger = logger;
        }

        /// <summary>
        /// Get all moods
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MoodDto>>> GetAllMoods()
        {
            var moods = await _moodService.GetAllMoodsAsync();
            return Ok(moods);
        }

        /// <summary>
        /// Get moods by category (sober or drunk)
        /// </summary>
        [HttpGet("category/{category}")]
        public async Task<ActionResult<List<MoodDto>>> GetMoodsByCategory(string category)
        {
            if (category.ToLower() != "sober" && category.ToLower() != "drunk")
            {
                return BadRequest(new { message = "Category must be 'sober' or 'drunk'" });
            }

            var moods = await _moodService.GetMoodsByCategoryAsync(category);
            return Ok(moods);
        }

        /// <summary>
        /// Get a specific mood by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MoodDto>> GetMoodById(Guid id)
        {
            var mood = await _moodService.GetMoodByIdAsync(id);

            if (mood == null)
            {
                return NotFound(new { message = "Mood not found" });
            }

            return Ok(mood);
        }
    }
}
