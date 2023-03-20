namespace UniversityMigration
{
    public class StudyType
    {
        public string Name { get; set; }
        public int NumberOStudents { get; set; }

        public StudyType(string name, int numberOStudents)
        {
            Name = name;
            NumberOStudents = numberOStudents;
        }
    }
}
