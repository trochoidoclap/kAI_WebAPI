namespace kAI_webAPI.Models.Subjects
{
    public class SubjectsGroup
    {
        // Mỗi subject là khóa ngoại đến Subjects.Id_subjects
        public int id_subgroup { get; set; }
        public int subject1 { get; set; }
        public int subject2 { get; set; }
        public int subject3 { get; set; }
        public int? subject4 { get; set; }
        public int? subject5 { get; set; }

        // Navigation properties
        public Subjects? subject1Navigation { get; set; }
        public Subjects? subject2Navigation { get; set; }
        public Subjects? subject3Navigation { get; set; }
        public Subjects? subject4Navigation { get; set; }
        public Subjects? subject5Navigation { get; set; }

    }
}
