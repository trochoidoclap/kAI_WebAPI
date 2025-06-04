using kAI_webAPI.Models.Subjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace kAI_webAPI.Dtos.Subjects
{
    public class SubjectsDto
    {
        public string Subject { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
    public class SubjectsGroupDto
    {

        public List<SubjectsDto> Subjects { get; set; } = new List<SubjectsDto>();
    }
}