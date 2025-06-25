using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Transcript
{
    public class Transcript
    {
        [Key]
        public int id_transcript { get; set; }
        public int id_user { get; set; }
        [ForeignKey("id_user")]
        public User.User? user { get; set; } = null;
        public DateTime date { get; set; } = DateTime.Now;
        public string content { get; set; } = string.Empty;
        [StringLength(4)]
        public string rating { get; set; } = string.Empty;

    }
}
