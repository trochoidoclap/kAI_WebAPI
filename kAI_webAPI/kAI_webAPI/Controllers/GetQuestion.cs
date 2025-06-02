using kAI_webAPI.Models.Question;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
namespace kAI_webAPI.Controllers
{
    public class GetQuestion : ControllerBase
    {
        private readonly Questioncontext _context;
        public GetQuestion(Questioncontext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/Questions/GetAll")]
        public IActionResult GetAllQuestions()
        {
            var questions = _context.Questions.ToList();
            return Ok(questions);
        }       
    }
}
