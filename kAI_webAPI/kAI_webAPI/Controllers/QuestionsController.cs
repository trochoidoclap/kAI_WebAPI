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
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IQuestionsRepository _questionsRepo;
        private readonly ILogger<QuestionsController> _logger;
        public QuestionsController(ApplicationDBContext context, IQuestionsRepository questionsRepo, ILogger<QuestionsController> logger)
        {
            _context = context;
            _questionsRepo = questionsRepo;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            _logger.LogInformation("Fetching all questions at {Time}", DateTime.UtcNow);
            var questions = await _questionsRepo.GetAllQuestionsAsync();
            var questionsDtos = questions.Select(s => s.ToGetQuestionsDto());
            return Ok(questionsDtos);
        }
    }
}
