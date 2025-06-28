using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kAI_webAPI.Models.Transcript
{
    public class Remark
    {
        [Key]
        public int id_remark { get; set; }
        [ForeignKey(nameof(Transcript))]
        public int id_transcript { get; set; }
        public Transcript Transcript { get; set; } = null!;
        public string content { get; set; } = string.Empty;
        public string choose { get; set; } = string.Empty;
    }
}
