using System.ComponentModel.DataAnnotations;

namespace MoodPlay.API.DTOs
{
    public class CreateSessionDto
    {
        [Required]
        public string Mode { get; set; } = string.Empty; // "sober" or "drunk"

        public Guid? MoodId { get; set; }

        public string? CustomPrompt { get; set; }

        [Range(1, 10, ErrorMessage = "Drink level must be between 1 and 10")]
        public int? DrinkLevel { get; set; }
    }
}