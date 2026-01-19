using MoodPlay.API.DTOs;

namespace MoodPlay.API.Services.Interfaces
{
    public interface ISongService
    {
        Task<List<SongDto>> GetSongsByMoodAsync(Guid moodId);
        Task<SongDto?> GetSongByIdAsync(Guid songId);
        Task<List<SongDto>> SearchSongsAsync(string searchTerm);
        Task<List<SongDto>> GetSongsBySessionAsync(Guid sessionId);
    }
}
