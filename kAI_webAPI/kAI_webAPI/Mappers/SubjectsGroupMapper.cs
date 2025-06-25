using kAI_webAPI.Dtos.Subjects;
using kAI_webAPI.Models.Subjects;

namespace kAI_webAPI.Mappers
{
    public static class SubjectsGroupMapper
    {
        public static SubjectsGroupFullDto ToGroupFullDto(this SubjectsGroup group)
        {
            return new SubjectsGroupFullDto
            {
                id_subgroup = group.id_subgroup,
                subject1 = group.subject1Navigation?.ToGetSubjectsDto() ?? new SubjectsDto(),
                subject2 = group.subject2Navigation?.ToGetSubjectsDto() ?? new SubjectsDto(),
                subject3 = group.subject3Navigation?.ToGetSubjectsDto() ?? new SubjectsDto(),
                subject4 = group.subject4Navigation?.ToGetSubjectsDto(),
                subject5 = group.subject5Navigation?.ToGetSubjectsDto()
            };
        }
    }
}
