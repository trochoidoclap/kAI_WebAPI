namespace kAI_webAPI.Dtos.Transcript
{
    public class GetTranscriptsDto
    {
        public int Id_transcript { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
    }
}
