using kAI_webAPI.Data;
using kAI_webAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kAI_webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public SubjectsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("groups-with-subjects")]
        public IActionResult GetGroupWithSubjects()
        {
            var groups = _context.SubjectsGroups
                .Include(g => g.subject1Navigation)
                .Include(g => g.subject2Navigation)
                .Include(g => g.subject3Navigation)
                .Include(g => g.subject4Navigation)
                .Include(g => g.subject5Navigation)
                .ToList();
            var result = groups.Select(g => g.ToGroupFullDto()).ToList();
            return Ok(result);
        }
    }
}
