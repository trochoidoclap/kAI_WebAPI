namespace kAI_webAPI.Models.Subjects
{
    public class Subjects
    {
        public int id_subjects { get; set; }
        public string subject_name { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        //public ICollection<SubjectsGroup> SubjectsGroups { get; set; } = new List<SubjectsGroup>();
    }
}
