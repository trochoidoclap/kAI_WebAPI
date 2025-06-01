using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaiApi.Models
{
    [Table("questions")]
    public class Question
    {
        [Key]
        [Column("id_question")]
        public int Id { get; set; }

        [Required]
        [Column("type")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public QuestionType? QuestionType { get; set; }

        [Column("content", TypeName = "text")]
        public string? Content { get; set; }
    }
}
