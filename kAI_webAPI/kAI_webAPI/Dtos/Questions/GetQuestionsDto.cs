using kAI_webAPI.Models.Question;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kAI_webAPI.Dtos.Questions
{
    public class GetQuestionsDto
    {
        // Nội dung câu hỏi cho người dùng
        public string Content { get; set; } = string.Empty;
    }
}
