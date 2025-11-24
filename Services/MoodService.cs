using Microsoft.EntityFrameworkCore;
using MoodPlay.API.Data;
using MoodPlay.API.DTOs;
using MoodPlay.API.Services.Interfaces;

namespace MoodPlay.API.Services
{
    public class MoodService : IMoodService
    {
        private readonly ApplicationDbContext _context;

        public MoodService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MoodDto>> GetAllMoodsAsync()
        {
            return await _context.Moods
                .Select(m => new MoodDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                    Description = m.Description,
                    IconName = m.IconName,
                    ColorHex = m.ColorHex,
                    Category = m.Category
                })
                .ToListAsync();
        }

        public async Task<List<MoodDto>> GetMoodsByCategoryAsync(string category)
        {
            return await _context.Moods
                .Where(m => m.Category == category.ToLower())
                .Select(m => new MoodDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Slug = m.Slug,
                    Description = m.Description,
                    IconName = m.IconName,
                    ColorHex = m.ColorHex,
                    Category = m.Category
                })
                .ToListAsync();
        }

        public async Task<MoodDto?> GetMoodByIdAsync(Guid moodId)
        {
            var mood = await _context.Moods.FindAsync(moodId);

            if (mood == null)
                return null;

            return new MoodDto
            {
                Id = mood.Id,
                Name = mood.Name,
                Slug = mood.Slug,
                Description = mood.Description,
                IconName = mood.IconName,
                ColorHex = mood.ColorHex,
                Category = mood.Category
            };
        }
    }
}