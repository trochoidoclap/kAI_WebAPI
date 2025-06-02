using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Question
{
    public class Questions_type
    {
        [Key]
        public int Id_questype { get; set; }

        [StringLength(15)]
        public string questype { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
    public class Question
    {
        [Key]
        public int Id_question { get; set; }
        public int Type { get; set; }
        [ForeignKey(nameof(Type))]
        public Questions_type? QuestionsType { get; set; }
        public string Content { get; set; } = string.Empty;
        public int Score { get; set; }

        
    }
}
