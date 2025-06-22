namespace kAI_webAPI.Dtos.Transcript
{
    public class CreateTranscriptRemarkDto
    {
        public int Id_transcript { get; set; }
        public int Id_user { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Choose { get; set; } = string.Empty;
    }
}
