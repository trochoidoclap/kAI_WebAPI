namespace kAI_webAPI.Dtos.Transcript
{
    public class GetTranscriptsDto
    {
        public int id_transcript { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string content { get; set; } = string.Empty;
        public string rating { get; set; } = string.Empty;
    }
}
