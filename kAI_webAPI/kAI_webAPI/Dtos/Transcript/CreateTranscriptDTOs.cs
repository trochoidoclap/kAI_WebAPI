using System.ComponentModel.DataAnnotations;
namespace kAI_webAPI.Dtos.Transcript
{
    public class CreateTranscriptDTOs
    {
        public DateTime date { get; set; } = DateTime.Now;
        public string content { get; set; } = string.Empty;
        public List<int> ratings { get; set; } = []; // 25 giá trị đánh giá
    }
}
