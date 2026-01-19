using MoodPlay.API.DTOs;
using MoodPlay.API.Models;

namespace MoodPlay.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto?> RegisterAsync(RegisterDto registerDto);
        Task<UserDto?> LoginAsync(LoginDto loginDto);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task<UserDto?> GetUserDtoByIdAsync(Guid userId);
    }
}