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
        [HttpPost("CreateTranscriptRemark")]
        public async Task<IActionResult> CreateTranscriptRemark([FromBody] CreateTranscriptRemarkDto createTranscriptRemarkDto)
        {
            if (createTranscriptRemarkDto == null)
                return BadRequest("Transcript remark data is null.");

            var IdUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (IdUserClaim == null || !int.TryParse(IdUserClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var Id_transcript = createTranscriptRemarkDto.Id_transcript;
            // Check if the transcript exists for the user
            var transcriptRemark = await _context.Transcript
                .FirstOrDefaultAsync(t => t.Id_transcript == Id_transcript && t.Id_user == userId);
            if (transcriptRemark == null)
                return NotFound("Transcript not found for the user.");

            // Validate the remark text and choice
            if (string.IsNullOrWhiteSpace(createTranscriptRemarkDto.Text) || string.IsNullOrWhiteSpace(createTranscriptRemarkDto.Choose))
                return BadRequest("Text and choice cannot be empty.");

            // Create a new Remark object and populate its properties
            var remark = new Remark
            {
                Id_transcript = transcriptRemark.Id_transcript,
                Text = createTranscriptRemarkDto.Text,
                Choose = createTranscriptRemarkDto.Choose
            };
            await _transciptRepo.AddRemarkAsync(remark);

            return Ok("Transcript remark added successfully.");
        }
        [HttpGet("GetUserTranscripts")]
        public async Task<IActionResult> GetUserTranscripts()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var getTranscripts = new List<GetTranscriptsDto>();

            var transcripts = await _transciptRepo.GetTranscriptsByUserId(userId);

            if (transcripts == null || transcripts.Count == 0)
                return NotFound("No transcripts found for the user.");

            return Ok(transcripts);
        }
        [HttpGet("GetTranscriptRemarks/{transcriptId}")]
        public async Task<IActionResult> GetTranscriptRemarks(int transcriptId)
        {
            var IdUserClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (IdUserClaim == null || !int.TryParse(IdUserClaim.Value, out int userId))
                return BadRequest("Invalid user ID.");

            var remarks = await _transciptRepo.GetTranscriptRemarksById(userId);
            if (remarks == null || remarks.Count == 0)
                return NotFound("No remarks found for the transcript.");
            return Ok(remarks);
        }
    }
}