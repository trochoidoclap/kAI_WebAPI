namespace kAI_webAPI.Dtos.Subjects
{
    public class SubjectsGroupFullDto
    {
        public int id_subgroup { get; set; }
        public SubjectsDto subject1 { get; set; } = new SubjectsDto();
        public SubjectsDto subject2 { get; set; } = new SubjectsDto();
        public SubjectsDto subject3 { get; set; } = new SubjectsDto();
        public SubjectsDto? subject4 { get; set; } = null;
        public SubjectsDto? subject5 { get; set; } = null;

    }
}
