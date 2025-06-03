using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kAI_webAPI.Models.Question;
namespace kAI_webAPI.Interfaces
{
    public interface IQuestionsRepository
    {
        Task<List<Question>> GetAllQuestionsAsync();
    }
}
