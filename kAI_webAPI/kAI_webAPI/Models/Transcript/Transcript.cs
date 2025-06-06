using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Transcript
{
    public class Transcript
    {
        [Key]
        public int Id_transcript { get; set; }
        public int Id_user { get; set; }
        [ForeignKey("Id_user")]
        public User.User? User { get; set; } = null;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        [StringLength(4)]
        public string Rating { get; set; } = string.Empty;
    }
}
