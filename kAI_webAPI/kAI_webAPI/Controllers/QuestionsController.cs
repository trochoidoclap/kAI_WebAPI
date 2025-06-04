using kAI_webAPI.Data;
using kAI_webAPI.Dtos.Questions;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Mappers;
using kAI_webAPI.Models.Question;
using kAI_webAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
namespace kAI_webAPI.Controllers
{
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IQuestionsRepository _questionsRepo;
        public QuestionsController(ApplicationDBContext context, IQuestionsRepository questionsRepo)
        {
            _context = context;
            _questionsRepo = questionsRepo;
        }
        [HttpGet]
        [Route("/Questions/GetAllQuestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _questionsRepo.GetAllQuestionsAsync();
            var questionsDtos = questions.Select(s => s.ToGetQuestionsDto());
            return Ok(questionsDtos);
        }
    }
}
