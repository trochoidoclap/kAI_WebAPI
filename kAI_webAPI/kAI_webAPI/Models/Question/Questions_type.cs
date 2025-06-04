using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Question
{
    public class Questions_type
    {
        [Key]
        public int Id_questype { get; set; }

        [StringLength(15)]
        public string Questype { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string Comment_vn { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
