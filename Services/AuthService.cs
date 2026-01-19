using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoodPlay.API.Data;
using MoodPlay.API.DTOs;
using MoodPlay.API.Models;
using MoodPlay.API.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoodPlay.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<UserDto?> RegisterAsync(RegisterDto registerDto)
        {
            // Check if user already exists
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email || u.Username == registerDto.Username))
            {
                return null;
            }

            // Hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Create user
            var user = new User
            {
                Email = registerDto.Email,
                Username = registerDto.Username,
                PasswordHash = passwordHash,
                DisplayName = registerDto.DisplayName ?? registerDto.Username
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                DisplayName = user.DisplayName,
                Token = token
            };
        }

        public async Task<UserDto?> LoginAsync(LoginDto loginDto)
        {
            // Try to find user by username or email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.EmailOrUsername || u.Email == loginDto.EmailOrUsername);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            // Update last login
            user.LastLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Generate JWT token
            var token = GenerateJwtToken(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                DisplayName = user.DisplayName,
                LastLogin = user.LastLogin,
                SpotifyId = user.SpotifyId,
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
            var jwtIssuer = _configuration["Jwt:Issuer"] ?? "MoodPlayAPI";
            var jwtAudience = _configuration["Jwt:Audience"] ?? "MoodPlayMobile";
            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "1440");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<UserDto?> GetUserDtoByIdAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                DisplayName = user.DisplayName,
                LastLogin = user.LastLogin,
                SpotifyId = user.SpotifyId
            };
        }
    }
}