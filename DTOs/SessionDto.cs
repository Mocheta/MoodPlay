namespace MoodPlay.API.DTOs
{
    public class SessionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Mode { get; set; } = string.Empty;
        public Guid? MoodId { get; set; }
        public string? MoodName { get; set; } // Include mood name for convenience
        public string? CustomPrompt { get; set; }
        public int? DrinkLevel { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public List<SongDto>? Songs { get; set; } // Optional: songs played in this session
        public MoodDto? Mood { get; internal set; }
    }
}