namespace kAI_webAPI.Dtos.Subjects
{
    public class SubjectsGroupFullDto
    {
        public int Id_subgroup { get; set; }
        public SubjectsDto Subject1 { get; set; } = new SubjectsDto();
        public SubjectsDto Subject2 { get; set; } = new SubjectsDto();
        public SubjectsDto Subject3 { get; set; } = new SubjectsDto();
        public SubjectsDto? Subject4 { get; set; } = null;
        public SubjectsDto? Subject5 { get; set; } = null;

    }
}
