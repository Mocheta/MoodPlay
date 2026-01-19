namespace MoodPlay.API.DTOs
{
    public class SongDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string? Album { get; set; }
        public int? DurationSeconds { get; set; }
        public string? SpotifyUri { get; set; }
        public int? ReleaseYear { get; set; }
        public string? Genre { get; set; }
        public decimal? EnergyLevel { get; set; }
        public decimal? Valence { get; set; }
        public decimal? Danceability { get; set; }
        public int? Tempo { get; set; }
        public decimal? RelevanceScore { get; set; } // From MoodSong junction table
    }
}