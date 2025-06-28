namespace kAI_webAPI.Dtos.Transcript
{
    public class CreateTranscriptRemarkDto
    {
        public int id_transcript { get; set; }
        //public int id_user { get; set; }
        public string content { get; set; } = string.Empty;
        public string choose { get; set; } = string.Empty;
    }
}
