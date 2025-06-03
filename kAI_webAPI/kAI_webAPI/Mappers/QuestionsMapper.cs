using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kAI_webAPI.Models.Question;
using kAI_webAPI.Dtos.Questions;
namespace kAI_webAPI.Mappers
{
    public static class QuestionsMapper
    {
        public static GetQuestionsDto ToGetQuestionsDto(this Question questionModel)
        {
            return new GetQuestionsDto
            {
                Content = questionModel.Content
            };
        }
    }
}
