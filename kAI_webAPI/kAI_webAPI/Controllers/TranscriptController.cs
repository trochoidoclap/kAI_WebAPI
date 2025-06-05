using AutoMapper;
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
    [Authorize(Roles = "Admin, User")]
    public class TranscriptController : ControllerBase
    {
        private readonly ITranscriptRepository _transcriptRepository;
        private readonly IMapper _mapper;
        public TranscriptController(ITranscriptRepository transcriptRepository, IMapper mapper)
        {
            _transcriptRepository = transcriptRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTranscript([FromBody] TranscriptDTOs transcriptDto)
        {
            if (transcriptDto == null)
                return BadRequest("Transcript data is null.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var ratings = transcriptDto.Ratings;
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

            await _transcriptRepository.AddTranscriptAsync(transcript); // Fixed method name
            return Ok("Lưu transcript thành công.");
        }
    }
}