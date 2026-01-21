using Microsoft.EntityFrameworkCore;
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
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return null;
            }

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
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
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Token = token
            };
        }

        public async Task<UserDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.EmailOrUsername || u.Username == loginDto.EmailOrUsername);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Token = token
            };
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<UserDto?> GetUserDtoByIdAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = _configuration["Jwt:Key"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!";
            var issuer = _configuration["Jwt:Issuer"] ?? "MoodPlayAPI";
            var audience = _configuration["Jwt:Audience"] ?? "MoodPlayApp";
            var expirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "1440");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
