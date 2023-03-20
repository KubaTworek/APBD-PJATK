using StudentsAPI.Model;


namespace StudentsAPI.Services
{
	public interface IStudentService
	{
		List<Student> GetAll();
        Student GetByIndex(String index);
		void UpdateByIndex(Student request, String index);
        Student Create(Student request);
		void Delete(String index);
	}

	public class StudentService : IStudentService
	{
		public StudentService()
		{
		}

		public List<Student> GetAll()
		{
			return new List<Student>();
		}

		public Student GetByIndex(String index)
		{
			return new Student();
		}

		public void UpdateByIndex(Student request, String index)
		{

		}

		public Student Create(Student request)
		{
            return new Student();
        }

        public void Delete(String index)
		{

		}
	}
}