namespace UniversityMigration
{
    public class Study
    {
        public string StudyMode { get; set; }
        public string StudyType { get; set; }

        public Study(string studyMode, string studyType)
        {
            StudyMode = studyMode;
            StudyType = studyType;
        }
    }
}
