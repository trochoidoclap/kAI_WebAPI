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
                Id_subgroup = group.Id_subgroup,
                Subject1 = group.Subject1Navigation?.ToGetSubjectsDto() ?? new SubjectsDto(),
                Subject2 = group.Subject2Navigation?.ToGetSubjectsDto() ?? new SubjectsDto(),
                Subject3 = group.Subject3Navigation?.ToGetSubjectsDto() ?? new SubjectsDto(),
                Subject4 = group.Subject4Navigation?.ToGetSubjectsDto(),
                Subject5 = group.Subject5Navigation?.ToGetSubjectsDto()
            };
        }
    }
}
