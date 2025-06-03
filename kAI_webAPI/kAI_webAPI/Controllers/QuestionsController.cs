using kAI_webAPI.Models.Question;
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
        private readonly Questioncontext _context;
        public QuestionsController(Questioncontext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/Questions/GetAll")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var questions = await _context.Questions.ToListAsync();
            return Ok(questions);
        }       
    }
}
