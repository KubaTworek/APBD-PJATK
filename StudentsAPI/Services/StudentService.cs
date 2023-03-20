using StudentsAPI.Model;
using StudentsAPI.DAO;

namespace StudentsAPI.Services
{
	public interface IStudentService
	{
		List<Student> GetAll();
        Student GetByIndex(String index);
		void UpdateByIndex(Student request, String index);
        void Create(Student request);
		void Delete(String index);
	}

	public class StudentService : IStudentService
	{
        private readonly IStudentDAO _studentDAO;

        public StudentService(IStudentDAO studentDAO)
		{
			_studentDAO = studentDAO;
		}

		public List<Student> GetAll()
		{
			List<Student> students = _studentDAO.GetAll();

            return students;
		}

		public Student GetByIndex(String index)
		{
			Student student = _studentDAO.GetAll().FirstOrDefault(x => x.Index == index);
			return student;
		}

		public void UpdateByIndex(Student request, String index)
		{
            List<Student> students = _studentDAO.GetAll().Where(s => s.Index != index).ToList();
            _studentDAO.saveAll(students);
            _studentDAO.Create(request);
        }

        public void Create(Student request)
		{
			_studentDAO.Create(request);
        }

        public void Delete(String index)
		{
            List<Student> students = _studentDAO.GetAll().Where(s => s.Index != index).ToList();
			_studentDAO.saveAll(students);
        }
	}
}