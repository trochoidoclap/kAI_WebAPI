using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Question
{
    public class Questions_type
    {
        [Key]
        public int id_questype { get; set; }

        [StringLength(15)]
        public string questype { get; set; } = string.Empty;
        public string comment { get; set; } = string.Empty;
        public string comment_vn { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
