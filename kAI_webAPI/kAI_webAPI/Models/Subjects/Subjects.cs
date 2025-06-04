using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Subjects
{
    public class Subjects_type
    {
        [Key]
        public int Id_subtype { get; set; }
        [StringLength(50)]
        public string Subtype { get; set; } = string.Empty;

        public ICollection<Subjects> Subjects { get; set; } = new List<Subjects>();
    }
    public class Subjects
    {
        [Key]
        public int Id_subjects { get; set; }
        public int Type { get; set; }
        [ForeignKey(nameof(Type))]
        public Subjects_type? SubjectsType { get; set; }
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty;

    }
    public class Subjects_group
    {
        [Key]
        public int Id_subgroup { get; set; }

        public int Subject1 { get; set; }
        [ForeignKey(nameof(Subject1))]
        public Subjects? Subjects1 { get; set; }

        public int Subject2 { get; set; }
        [ForeignKey(nameof(Subject2))]
        public Subjects? Subjects2 { get; set; }

        public int Subject3 { get; set; }
        [ForeignKey(nameof(Subject3))]
        public Subjects? Subjects3 { get; set; }

        public int? Subject4 { get; set; }
        [ForeignKey(nameof(Subject4))]
        public Subjects? Subjects4 { get; set; }

        public int? Subject5 { get; set; }
        [ForeignKey(nameof(Subject5))]
        public Subjects? Subjects5 { get; set; }
    }

}
