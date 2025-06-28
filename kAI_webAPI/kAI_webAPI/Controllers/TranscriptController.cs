using kAI_webAPI.Data;
using kAI_webAPI.Dtos.Transcript;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.Transcript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
namespace kAI_webAPI.Controllers
{
    [Route("api/transcripts")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")] // Phân quyền có thể tùy chỉnh nếu cần
    [Authorize] // Chỉ cần xác thực người dùng, không cần phân quyền cụ thể
    public class TranscriptController : ControllerBase
    {
        private readonly ITranscriptRepository _transciptRepo;
        private readonly ApplicationDBContext _context;
        private readonly ILogger<TranscriptController> _logger;
        public TranscriptController(ITranscriptRepository transcriptRepo, ApplicationDBContext context, ILogger<TranscriptController> logger)
        {
            _transciptRepo = transcriptRepo;
            _context = context;
            _logger = logger;
        }

        private int GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                _logger.LogWarning("Invalid user ID claim at {Time}", DateTime.UtcNow);
                throw new UnauthorizedAccessException("Invalid user ID.");
            }
            return userId;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTranscript([FromBody] CreateTranscriptDTOs createTranscriptDto)
        {
            _logger.LogInformation("Creating a new transcript for user {UserId} at {Time}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value, DateTime.UtcNow);
            if (createTranscriptDto == null)
            {
                _logger.LogWarning("CreateTranscript called with null DTO at {Time}", DateTime.UtcNow);
                return BadRequest("Transcript data is null.");
            }

            var IdUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (IdUserClaim == null || !int.TryParse(IdUserClaim.Value, out int userId))
            {
                _logger.LogWarning("Invalid user ID claim at {Time}", DateTime.UtcNow);
                return BadRequest("Invalid user ID.");
            }

            var ratings = createTranscriptDto.ratings;
            if (ratings.Count != 25)
            {
                _logger.LogWarning("Invalid number of ratings provided: {Count} at {Time}", ratings.Count, DateTime.UtcNow);
                if (ratings.Count < 25)
                    return BadRequest("Phải có đủ 25 giá trị rating (1–5).");
            }

            for (int i = 0; i < ratings.Count; i++)
                if (ratings[i] < 1 || ratings[i] > 5)
                {
                    _logger.LogWarning("Invalid rating value at position {Position}: {Rating} at {Time}", i + 1, ratings[i], DateTime.UtcNow);
                    return BadRequest($"Giá trị rating tại vị trí {i + 1} phải nằm trong khoảng từ 1 đến 5.");
                }

            // Tạo content dạng 01X02X...25X
            var sbContent = new StringBuilder();
            for (int i = 0; i < ratings.Count; i++)
                sbContent.Append($"{(i + 1):D2}{ratings[i]}");

            var transcript = new Transcript
            {
                id_user = userId,
                date = DateTime.UtcNow,
                content = sbContent.ToString(),
                rating = "MBTI"
            };

            await _transciptRepo.AddTranscriptAsync(transcript);
            _logger.LogInformation("Transcript created successfully for user {UserId} at {Time}", userId, DateTime.UtcNow);
            return Ok("Lưu transcript thành công.");
        }
        [HttpPost("remarks")]
        public async Task<IActionResult> CreateTranscriptRemark([FromBody] CreateTranscriptRemarkDto createTranscriptRemarkDto)
        {
            _logger.LogInformation("Creating a new transcript remark for user {UserId} at {Time}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value, DateTime.UtcNow);
            if (createTranscriptRemarkDto == null)
                return BadRequest("Transcript remark data is null.");

            var IdUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (IdUserClaim == null || !int.TryParse(IdUserClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var Id_transcript = createTranscriptRemarkDto.id_transcript;
            // Check if the transcript exists for the user
            var transcriptRemark = await _context.Transcript
                .FirstOrDefaultAsync(t => t.id_transcript == Id_transcript && t.id_user == userId);
            if (transcriptRemark == null)
            {
                _logger.LogWarning("Transcript not found for user {UserId} at {Time}", userId, DateTime.UtcNow);
                return NotFound($"Transcript not found for the user {userId}.");
            }

            // Validate the remark text and choice
            if (string.IsNullOrWhiteSpace(createTranscriptRemarkDto.content) || string.IsNullOrWhiteSpace(createTranscriptRemarkDto.choose))
                return BadRequest("Text and choice cannot be empty.");

            // Create a new Remark object and populate its properties
            var remark = new Remark
            {
                id_transcript = transcriptRemark.id_transcript,
                content = createTranscriptRemarkDto.content,
                choose = createTranscriptRemarkDto.choose
            };
            await _transciptRepo.AddRemarkAsync(remark);
            _logger.LogInformation("Transcript remark created successfully for user {UserId} at {Time}", userId, DateTime.UtcNow);
            return Ok("Transcript remark added successfully.");
        }
        [HttpGet]
        public async Task<IActionResult> GetUserTranscripts()
        {
            _logger.LogInformation("Retrieving transcripts for user {UserId} at {Time}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value, DateTime.UtcNow);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var getTranscripts = new List<GetTranscriptsDto>();

            var transcripts = await _transciptRepo.GetTranscriptsByUserId(userId);

            if (transcripts == null || transcripts.Count == 0)
                return NotFound("No transcripts found for the user.");
            _logger.LogInformation("Transcripts retrieved successfully for user {UserId} at {Time}", userId, DateTime.UtcNow);
            return Ok(transcripts);
        }
        [HttpGet("remarks")]
        public async Task<IActionResult> GetUserTranscriptRemarks(int transcriptId)
        {
            _logger.LogInformation("Retrieving remarks for transcript {TranscriptId} at {Time}", transcriptId, DateTime.UtcNow);
            var IdUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (IdUserClaim == null || !int.TryParse(IdUserClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var remarks = await _transciptRepo.GetTranscriptRemarksById(userId);
            if (remarks == null || remarks.Count == 0)
                return NotFound("No remarks found for the transcript.");
            _logger.LogInformation("Remarks retrieved successfully for transcript {TranscriptId} at {Time}", transcriptId, DateTime.UtcNow);
            return Ok(remarks);
        }
    }
}