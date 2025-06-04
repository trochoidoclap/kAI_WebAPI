using kAI_webAPI.Models.Subjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace kAI_webAPI.Dtos.Subjects
{
    public class SubjectsDTOs
    {
        public string Subject { get; set; } = string.Empty;
        public string Subtype { get; set; } = string.Empty;
    }
    public class SubjectsGroupDTOs
    {

        public List<SubjectsDTOs> Subjects { get; set; } = new List<SubjectsDTOs>();
    }
}