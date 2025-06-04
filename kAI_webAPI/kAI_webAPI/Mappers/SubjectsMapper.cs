using kAI_webAPI.Dtos.Subjects;

namespace kAI_webAPI.Mappers
{
    public static class SubjectsMapper
        {
        public static SubjectsDto ToGetSubjectsDto(this SubjectsDto subjectsModel)
            {
            return new SubjectsDto
            {
                Subject = subjectsModel.Subject,
                Type = subjectsModel.Type
            };
        }
        public static SubjectsGroupDto ToGetSubjectsGroupDto(this SubjectsGroupDto subjectsGroupModel)
        {
            return new SubjectsGroupDto
            {
                Subjects = subjectsGroupModel.Subjects.Select(s => new SubjectsDto
                {
                    Subject = s.Subject,
                    Type = s.Type
                }).ToList()
            };
        }
    }
}
