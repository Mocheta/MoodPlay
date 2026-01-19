using Microsoft.EntityFrameworkCore;
using MoodPlay.API.Data;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;

namespace MoodPlay.API.Services
{
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _context;

        public SongService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SongDto>> GetSongsByMoodAsync(Guid moodId)
        {
            return await _context.MoodSongs
                .Where(ms => ms.MoodId == moodId)
                .Include(ms => ms.Song)
                .OrderByDescending(ms => ms.RelevanceScore)
                .Select(ms => new SongDto
                {
                    Id = ms.Song.Id,
                    Title = ms.Song.Title,
                    Artist = ms.Song.Artist,
                    Album = ms.Song.Album,
                    DurationSeconds = ms.Song.DurationSeconds,
                    SpotifyUri = ms.Song.SpotifyUri,
                    ReleaseYear = ms.Song.ReleaseYear,
                    Genre = ms.Song.Genre,
                    EnergyLevel = ms.Song.EnergyLevel,
                    Valence = ms.Song.Valence,
                    Danceability = ms.Song.Danceability,
                    Tempo = ms.Song.Tempo,
                    RelevanceScore = ms.RelevanceScore
                })
                .ToListAsync();
        }

        public async Task<SongDto?> GetSongByIdAsync(Guid songId)
        {
            var song = await _context.Songs.FindAsync(songId);

            if (song == null)
                return null;

            return new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Album = song.Album,
                DurationSeconds = song.DurationSeconds,
                SpotifyUri = song.SpotifyUri,
                ReleaseYear = song.ReleaseYear,
                Genre = song.Genre,
                EnergyLevel = song.EnergyLevel,
                Valence = song.Valence,
                Danceability = song.Danceability,
                Tempo = song.Tempo
            };
        }

        public async Task<List<SongDto>> SearchSongsAsync(string searchTerm)
        {
            var term = $"%{searchTerm}%";
            return await _context.Songs
                .Where(s => EF.Functions.ILike(s.Title, term) || 
                           EF.Functions.ILike(s.Artist, term) ||
                           (s.Album != null && EF.Functions.ILike(s.Album, term)))
                .Select(s => new SongDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Artist = s.Artist,
                    Album = s.Album,
                    DurationSeconds = s.DurationSeconds,
                    SpotifyUri = s.SpotifyUri,
                    ReleaseYear = s.ReleaseYear,
                    Genre = s.Genre,
                    EnergyLevel = s.EnergyLevel,
                    Valence = s.Valence,
                    Danceability = s.Danceability,
                    Tempo = s.Tempo
                })
                .Take(50)
                .ToListAsync();
        }

        public async Task<List<SongDto>> GetSongsBySessionAsync(Guid sessionId)
        {
            var session = await _context.UserSessions
                .Include(s => s.Mood)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null || session.MoodId == null)
                return new List<SongDto>();

            return await GetSongsByMoodAsync(session.MoodId.Value);
        }
    }
}
