using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kAI_webAPI.Models.Transcript
{
    public class Remark
    {
        [Key]
        public int Id_remark { get; set; }
        [ForeignKey(nameof(Transcript))]
        public int Id_transcript { get; set; }
        public Transcript Transcript { get; set; } = null!;
        public string Text { get; set; } = string.Empty;
        public string Choose { get; set; } = string.Empty;
    }
}
