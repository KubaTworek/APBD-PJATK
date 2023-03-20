namespace UniversityMigration
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IndexNumber { get; set; }
        public string Birthdate { get; set; }
        public string Email { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Study Study { get; set; }

        public Student(string firstName, string lastName, int indexNumber,
                       string birthdate, string email, string motherName,
                       string fatherName, Study study)
        {
            FirstName = firstName;
            LastName = lastName;
            IndexNumber = indexNumber;
            Birthdate = birthdate;
            Email = email;
            MotherName = motherName;
            FatherName = fatherName;
            Study = study;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({IndexNumber})";
        }
    }
}
