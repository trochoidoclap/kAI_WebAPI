using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace kAI_webAPI.Models.Subjects
{

    public class Subjects
    {
        [Key]
        public int Id_subjects { get; set; }
        [StringLength(50)]
        public string Subject { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Type { get; set; } = string.Empty;
    }
    

}
