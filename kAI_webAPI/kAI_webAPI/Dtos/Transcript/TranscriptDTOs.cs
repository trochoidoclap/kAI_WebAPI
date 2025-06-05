using System.ComponentModel.DataAnnotations;
namespace kAI_webAPI.Dtos.Transcript
{
    public class TranscriptDTOs
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        public List<int> Ratings { get; set; } = []; // 25 giá trị đánh giá
        [StringLength(4)]
        public string Rating { get; set; } = string.Empty;
    }
}
