using kAI_webAPI.Dtos.Subjects;
using kAI_webAPI.Models.Subjects;

namespace kAI_webAPI.Mappers
{
    public static class SubjectsMapper
    {
        public static SubjectsDto ToGetSubjectsDto(this Subjects subject)
        {
            return new SubjectsDto
            {
                subject_name = subject.subject_name,
                type = subject.type
            };
        }
    }
}
