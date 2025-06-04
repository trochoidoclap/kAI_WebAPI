using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Question
{
    
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
