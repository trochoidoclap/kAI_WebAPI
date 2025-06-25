using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Question
{
    
    public class Question
    {
        [Key]
        public int id_question { get; set; }
        public int type { get; set; }
        [ForeignKey(nameof(Type))]
        public Questions_type? QuestionsType { get; set; }
        public string type_MBTI { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public int score { get; set; }
    }
}
