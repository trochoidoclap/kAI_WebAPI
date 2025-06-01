using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaiApi.Models
{
    [Table("subjects_group")]
    public class SubjectsGroup
    {
        [Key]
        [Column("id_subgroup")]
        public int Id { get; set; }

        [Required]
        [Column("subject1")]
        public int Subject1Id { get; set; }
        [ForeignKey("Subject1Id")]
        public Subject? Subject1 { get; set; }

        [Required]
        [Column("subject2")]
        public int Subject2Id { get; set; }
        [ForeignKey("Subject2Id")]
        public Subject? Subject2 { get; set; }

        [Required]
        [Column("subject3")]
        public int Subject3Id { get; set; }
        [ForeignKey("Subject3Id")]
        public Subject? Subject3 { get; set; }

        [Column("subject4")]
        public int? Subject4Id { get; set; }
        [ForeignKey("Subject4Id")]
        public Subject? Subject4 { get; set; }

        [Column("subject5")]
        public int? Subject5Id { get; set; }
        [ForeignKey("Subject5Id")]
        public Subject? Subject5 { get; set; }
    }
}
