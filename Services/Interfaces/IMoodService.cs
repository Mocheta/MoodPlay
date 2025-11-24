using MoodPlay.API.DTOs;
using MoodPlay.API.Models;

namespace MoodPlay.API.Services.Interfaces
{
    public interface IMoodService
    {
        Task<List<MoodDto>> GetAllMoodsAsync();
        Task<List<MoodDto>> GetMoodsByCategoryAsync(string category);
        Task<MoodDto?> GetMoodByIdAsync(Guid moodId);
    }
}
