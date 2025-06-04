namespace kAI_webAPI.Models.Subjects
{
    public class SubjectsGroup
    {
        // Mỗi subject là khóa ngoại đến Subjects.Id_subjects
        public int Id_subgroup { get; set; }
        public int Subject1 { get; set; }
        public int Subject2 { get; set; }
        public int Subject3 { get; set; }
        public int? Subject4 { get; set; }
        public int? Subject5 { get; set; }

        // Navigation properties
        public Subjects? Subject1Navigation { get; set; }
        public Subjects? Subject2Navigation { get; set; }
        public Subjects? Subject3Navigation { get; set; }
        public Subjects? Subject4Navigation { get; set; }
        public Subjects? Subject5Navigation { get; set; }

    }
}
