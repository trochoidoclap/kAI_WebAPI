using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaiApi.Models
{
    [Table("questions_type")]
    public class QuestionType
    {
        [Key]
        [Column("id_questype")]
        public int Id { get; set; }

        [Required]
        [Column("questype", TypeName = "char(50)")]
        public string Questype { get; set; } = null!;

        [Column("comment", TypeName = "tinytext")]
        public string? Comment { get; set; }

        [Column("commentVN", TypeName = "tinytext")]
        public string? CommentVN { get; set; }

        // Quan hệ 1-N với bảng questions
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
