using StudentsAPI.Model;
using StudentsAPI.DAO;
using System;

namespace StudentsAPI.Services
{
	public interface IStudentService
	{
		List<Student> GetAll();
        Student GetByIndex(String index);
        bool UpdateByIndex(Student request, String index);
        bool Create(Student request);
        bool Delete(String index);
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
            return _studentDAO.GetAll();
        }

        public Student GetByIndex(string index)
        {
            return _studentDAO.GetAll().SingleOrDefault(x => x.Index == index);
        }

        public bool UpdateByIndex(Student request, string index)
        {
            if(GetByIndex(index) == null)
            {
                return false;
            } 
            else
            {
                List<Student> students = _studentDAO.GetAll().Where(s => s.Index != index).ToList();
                students.Add(request);
                _studentDAO.saveAll(students);
                return true;
            }
        }

        public bool Create(Student request)
        {
            if (isValidRequest(request))
            {
                return false;
            }
            else
            {
                _studentDAO.Create(request);
                return true;
            }
        }

        public bool Delete(string index)
        {
            if (GetByIndex(index) == null)
            {
                return false;
            }
            else
            {
                List<Student> students = _studentDAO.GetAll().Where(s => s.Index != index).ToList();
                _studentDAO.saveAll(students);
                return true;
            }
        }

        private bool isValidRequest(Student request)
        {
            return request.FirstName != null
                && request.LastName != null
                && request.Index != null
                && request.Birthdate != null
                && request.StudyType != null
                && request.StudyMode != null
                && request.Email != null
                && request.FatherName != null
                && request.MotherName != null
                && GetByIndex(request.Index) != null;
        }
    }
}