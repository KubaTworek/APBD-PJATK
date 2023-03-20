namespace UniversityMigration
{
    public class University
    {
        public string CreatedAt { get; set; }
        public string Author { get; set; }
        public List<Student> Students { get; set; }
        public List<StudyType> ActiveStudies { get; set; }

        public University(List<Student> students, List<StudyType> activeStudies)
        {
            CreatedAt = DateTime.Now.ToString("dd.MM.yyyy");
            Author = "Jaub Tworek";
            Students = students;
            ActiveStudies = activeStudies;
        }
    }
}
