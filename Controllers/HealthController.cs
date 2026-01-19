using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoodPlay.API.Data;

namespace MoodPlay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HealthController> _logger;

        public HealthController(ApplicationDbContext context, ILogger<HealthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Check API health
        /// </summary>
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }

        /// <summary>
        /// Test database connection and verify schema
        /// </summary>
        [HttpGet("database")]
        public async Task<ActionResult<object>> TestDatabase()
        {
            try
            {
                // Test database connection
                var canConnect = await _context.Database.CanConnectAsync();
                if (!canConnect)
                {
                    return StatusCode(503, new { 
                        status = "error", 
                        message = "Cannot connect to database",
                        connectionString = "Check your connection string in appsettings.json"
                    });
                }

                // Check if tables exist
                var usersTableExists = await _context.Database.ExecuteSqlRawAsync(
                    "SELECT 1 FROM information_schema.tables WHERE table_name = 'users'") != null;

                // Try to query each table to verify schema
                var usersCount = await _context.Users.CountAsync();
                var moodsCount = await _context.Moods.CountAsync();
                var songsCount = await _context.Songs.CountAsync();
                var sessionsCount = await _context.UserSessions.CountAsync();
                var moodSongsCount = await _context.MoodSongs.CountAsync();

                // Check for required columns in users table
                var userColumns = new List<string>();
                try
                {
                    var testUser = await _context.Users.FirstOrDefaultAsync();
                    if (testUser != null)
                    {
                        userColumns.Add("✓ Users table accessible");
                    }
                    else
                    {
                        userColumns.Add("✓ Users table exists (empty)");
                    }
                }
                catch (Exception ex)
                {
                    userColumns.Add($"✗ Users table error: {ex.Message}");
                }

                return Ok(new
                {
                    status = "success",
                    database = "connected",
                    tables = new
                    {
                        users = new { exists = true, count = usersCount },
                        moods = new { exists = true, count = moodsCount },
                        songs = new { exists = true, count = songsCount },
                        userSessions = new { exists = true, count = sessionsCount },
                        moodSongs = new { exists = true, count = moodSongsCount }
                    },
                    schema = new
                    {
                        usersTable = userColumns,
                        message = "All tables are accessible"
                    },
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database health check failed");
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message,
                    details = ex.InnerException?.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        /// <summary>
        /// Get database schema information
        /// </summary>
        [HttpGet("schema")]
        public async Task<ActionResult<object>> GetSchema()
        {
            try
            {
                var schemaInfo = new
                {
                    users = await GetTableColumns("users"),
                    moods = await GetTableColumns("moods"),
                    songs = await GetTableColumns("songs"),
                    user_sessions = await GetTableColumns("user_sessions"),
                    mood_songs = await GetTableColumns("mood_songs")
                };

                return Ok(schemaInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Schema check failed");
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }

        private async Task<List<object>> GetTableColumns(string tableName)
        {
            var columns = new List<object>();
            var query = $@"
                SELECT column_name, data_type, character_maximum_length, is_nullable
                FROM information_schema.columns
                WHERE table_name = '{tableName}'
                ORDER BY ordinal_position";

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            await _context.Database.OpenConnectionAsync();

            try
            {
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    columns.Add(new
                    {
                        name = reader.GetString(0),
                        type = reader.GetString(1),
                        maxLength = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2),
                        nullable = reader.GetString(3) == "YES"
                    });
                }
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }

            return columns;
        }
    }
}
