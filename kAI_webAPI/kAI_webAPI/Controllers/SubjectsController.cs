using kAI_webAPI.Data;
using kAI_webAPI.Dtos.Subjects;
using kAI_webAPI.Models.Subjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kAI_webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsGroupController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public SubjectsGroupController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            var groupEntity = await _context.Subjects_groups
                .Include(g => g.Subjects1)
                .Include(g => g.Subjects2)
                .Include(g => g.Subjects3)
                .Include(g => g.Subjects4)
                .Include(g => g.Subjects5)
                .FirstOrDefaultAsync(g => g.Id_subgroup == id);

            if (groupEntity == null)
                return NotFound("Group not found.");

            var dto = new SubjectsGroupDTOs
            {
                Subjects = new List<SubjectsDTOs>()
            };

            if (groupEntity.Subjects1 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects1.Subject,
                    Type = groupEntity.Subjects1.Type ?? string.Empty 
                });
            }

            if (groupEntity.Subjects2 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects2.Subject,
                    Type = groupEntity.Subjects2.Type ?? string.Empty 
                });
            }

            if (groupEntity.Subjects3 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects3.Subject,
                    Type = groupEntity.Subjects3.Type ?? string.Empty 
                });
            }

            if (groupEntity.Subjects4 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects4.Subject!,
                    Type = groupEntity.Subjects4.Type ?? string.Empty 
                });
            }

            if (groupEntity.Subjects5 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects5.Subject!,
                    Type = groupEntity.Subjects5.Type ?? string.Empty 
                });
            }

            return Ok(dto);
        }
    }
}




