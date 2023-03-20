using StudentsAPI.Model;

namespace StudentsAPI.DAO
{
    public interface IStudentDAO
    {
        List<Student> GetAll();
        void Create(Student student);
        void saveAll(List<Student> students);
    }

    public class StudentDAO : IStudentDAO
    {
        public StudentDAO() { }

        public List<Student> GetAll()
        {
            var students = new List<Student>();

            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Data", "students.csv"));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var student = new Student
                    (
                        values[0],
                        values[1],
                        values[2],
                        values[3],
                        values[4],
                        values[5],
                        values[6],
                        values[7],
                        values[8]
                    );

                students.Add(student);
            }
            return students;
        }

        public void Create(Student student)
        {
            string studentString = student.ToString();
            using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "Data", "students.csv"), true))
            {
                writer.WriteLine(studentString);
            }
        }

        public void saveAll(List<Student> students)
        {
            using (var writer = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "Data", "students.csv")))
            {
                foreach (var student in students)
                {
                    writer.WriteLine(student.ToString());
                }
            }
        }
    }
}
