namespace StudentsAPI.Model
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Index { get; set; }
        public string Birthdate { get; set; }
        public string StudyType { get; set; }
        public string StudyMode { get; set; }
        public string Email { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public Student(string firstName, string lastName, string index, string birthdate, string studyType, string studyMode, string email, string fatherName, string motherName)
        {
            FirstName = firstName;
            LastName = lastName;
            Index = index;
            Birthdate = birthdate;
            StudyType = studyType;
            StudyMode = studyMode;
            Email = email;
            FatherName = fatherName;
            MotherName = motherName;
        }
    }
}
