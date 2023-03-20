using StudentsAPI.Model;
using System.Globalization;

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
        private readonly string _filePath;

        public StudentDAO()
        {
            _filePath = Path.Combine(Environment.CurrentDirectory, "Data", "students.csv");
        }

        public List<Student> GetAll()
        {
            var students = new List<Student>();

            using var reader = new StreamReader(_filePath);
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
            using var writer = new StreamWriter(_filePath, true);
            writer.WriteLine(student.ToString());
        }

        public void saveAll(List<Student> students)
        {
            using var writer = new StreamWriter(_filePath);
            foreach (var student in students)
            {
                writer.WriteLine(student.ToString());
            }
        }
    }
}
