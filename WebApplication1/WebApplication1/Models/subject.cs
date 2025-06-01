using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaiApi.Models
{
    [Table("subjects")]
    public class Subject
    {
        [Key]
        [Column("id_subjects")]
        public int Id { get; set; }

        [Required]
        [Column("subject", TypeName = "varchar(50)")]
        public string Name { get; set; } = null!;

        [Required]
        [Column("type", TypeName = "varchar(50)")]
        public string Type { get; set; } = null!;

        // Quan hệ N-? với bảng subjects_group (phía bên subjects_group sẽ có FK)
        public ICollection<SubjectsGroup> SubjectsGroupsAs1 { get; set; } = new List<SubjectsGroup>();
        public ICollection<SubjectsGroup> SubjectsGroupsAs2 { get; set; } = new List<SubjectsGroup>();
        public ICollection<SubjectsGroup> SubjectsGroupsAs3 { get; set; } = new List<SubjectsGroup>();
        public ICollection<SubjectsGroup>? SubjectsGroupsAs4 { get; set; }
        public ICollection<SubjectsGroup>? SubjectsGroupsAs5 { get; set; }
    }
}
