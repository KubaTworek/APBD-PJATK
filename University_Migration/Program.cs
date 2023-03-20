using System.Text.Json;

namespace UniversityMigration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length < 3)
                {
                    throw new ArgumentException("Nieprawidłowa liczba argumentów. Użyj: nazwa_pliku_csv ścieżka_do_json format_wyjścia");
                }

                string csvPath = args[0];
                string jsonAdress = args[1];
                string format = args[2];

                if (!File.Exists(csvPath))
                {
                    throw new FileNotFoundException("Podany plik nie istnieje.", csvPath);
                }

                var students = new List<Student>();

                using var reader = new StreamReader(csvPath);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (IsValidStudentData(values))
                    {
                        var study = new Study(values[2], values[3]);

                        var student = new Student(GetFormattedName(values[0]), GetFormattedName(values[1]),
                                                  int.Parse(values[4]), DateTime.Parse(values[5]).ToString("dd.MM.yyyy"),
                                                  values[6], values[7], values[8], study);

                        if (IsDuplicateStudent(student, students))
                        {
                            LogMessageToFile($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}:  Student with name {values[0]} {values[1]} already exists");
                        }
                        else
                        {
                            students.Add(student);
                        }
                    }
                    else
                    {
                        LogMessageToFile($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: Wrong data was prepared for Student {values[0]} {values[1]}");
                    }
                }

                var studyTypes = GetStudyTypes(students);
                var university = new University(students, studyTypes);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                if (format == "json")
                {
                    var jsonString = JsonSerializer.Serialize(university, options);
                    using var writer = new StreamWriter(jsonAdress + "data.json");
                    writer.Write(jsonString);
                }
                else
                {
                    throw new ArgumentException("Nieznany format wyjścia. Dostępne formaty: json");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                LogMessageToFile($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {ex.GetType().Name}: {ex.Message}");
            }
        }

        private static List<StudyType> GetStudyTypes(List<Student> students)
        {
            return students.GroupBy(s => s.Study.StudyType)
                           .Select(group => new StudyType(group.Key, group.Count()))
                           .ToList();
        }

        private static bool IsValidStudentData(string[] values)
        {
            return values.All(v => !string.IsNullOrWhiteSpace(v));
        }

        private static bool IsDuplicateStudent(Student student, List<Student> students)
        {
            return students.Any(s => IsSameStudent(s, student));
        }

        private static bool IsSameStudent(Student student1, Student student2)
        {
            return student1.IndexNumber == student2.IndexNumber &&
                   student1.FirstName == student2.FirstName &&
                   student1.LastName == student2.LastName;
        }

        private static string GetFormattedName(string name)
        {
            return new string(name.Where(c => char.IsLetter(c)).ToArray());
        }

        private static void LogMessageToFile(string message)
        {
            using var writer = new StreamWriter("log.txt", true);
            writer.WriteLine(message);
        }
    }
}
