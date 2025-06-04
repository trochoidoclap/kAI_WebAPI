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
        private readonly Subjectscontext _context;

        public SubjectsGroupController(Subjectscontext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {

            var groupEntity = await _context.Subjects_groups
                .Include(g => g.Subjects1)
                    .ThenInclude(s => s!.SubjectsType)
                .Include(g => g.Subjects2)
                    .ThenInclude(s => s!.SubjectsType)
                .Include(g => g.Subjects3)
                    .ThenInclude(s => s!.SubjectsType)
                .Include(g => g.Subjects4)
                    .ThenInclude(s => s!.SubjectsType)
                .Include(g => g.Subjects5)
                    .ThenInclude(s => s!.SubjectsType)
                .FirstOrDefaultAsync(g => g.Id_subgroup == id);

            if (groupEntity == null)
                return NotFound("Group not found.");


            var dto = new SubjectsGroupDTOs();


            if (groupEntity.Subjects1 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects1.Subject,
                    Subtype = groupEntity.Subjects1.SubjectsType!.Subtype
                });
            }

            if (groupEntity.Subjects2 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects2.Subject,
                    Subtype = groupEntity.Subjects2.SubjectsType!.Subtype
                });
            }

            if (groupEntity.Subjects3 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects3.Subject,
                    Subtype = groupEntity.Subjects3.SubjectsType!.Subtype
                });
            }

            if (groupEntity.Subjects4 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects4.Subject!,
                    Subtype = groupEntity.Subjects4.SubjectsType!.Subtype
                });
            }

            if (groupEntity.Subjects5 != null)
            {
                dto.Subjects.Add(new SubjectsDTOs
                {
                    Subject = groupEntity.Subjects5.Subject!,
                    Subtype = groupEntity.Subjects5.SubjectsType!.Subtype
                });
            }

            return Ok(dto);
        }
    }
}




