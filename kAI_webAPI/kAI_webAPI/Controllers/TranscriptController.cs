using kAI_webAPI.Data;
using kAI_webAPI.Dtos.Transcript;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.Transcript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
namespace kAI_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")] // Phân quyền có thể tùy chỉnh nếu cần
    [Authorize] // Chỉ cần xác thực người dùng, không cần phân quyền cụ thể
    public class TranscriptController : ControllerBase
    {
        private readonly ITranscriptRepository _transciptRepo;
        private readonly ApplicationDBContext _context;
        public TranscriptController(ITranscriptRepository transcriptRepo, ApplicationDBContext context)
        {
            _transciptRepo = transcriptRepo;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTranscript([FromBody] CreateTranscriptDTOs createTranscriptDto)
        {
            if (createTranscriptDto == null)
                return BadRequest("Transcript data is null.");

            var IdUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (IdUserClaim == null || !int.TryParse(IdUserClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var ratings = createTranscriptDto.Ratings;
            if (ratings.Count != 25)
                return BadRequest("Phải có đúng 25 giá trị rating (1–5).");

            for (int i = 0; i < ratings.Count; i++)
                if (ratings[i] < 1 || ratings[i] > 5)
                    return BadRequest($"Giá trị rating tại vị trí {i + 1} phải nằm trong khoảng từ 1 đến 5.");

            // Tạo content dạng 01X02X...25X
            var sbContent = new StringBuilder();
            for (int i = 0; i < ratings.Count; i++)
                sbContent.Append($"{(i + 1):D2}{ratings[i]}");

            var transcript = new Transcript
            {
                Id_user = userId,
                Date = DateTime.UtcNow,
                Content = sbContent.ToString(),
                Rating = "MBTI"
            };

            await _transciptRepo.AddTranscriptAsync(transcript);
            return Ok("Lưu transcript thành công.");
        }
        [HttpGet("GetUserTranscripts")]
        public async Task<IActionResult> GetUserTranscripts()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var transcripts = await _transciptRepo.GetTranscriptsByUserId(userId);

            if (transcripts == null || transcripts.Count == 0)
                return NotFound("No transcripts found for the user.");

            return Ok(transcripts);
        }
    }
}