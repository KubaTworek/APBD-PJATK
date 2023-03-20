using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Services;
using StudentsAPI.Model;

namespace StudentsAPI.Controllers
{
	[ApiController]
	[Route("api/students")]
	public class StudentController: ControllerBase
	{
		private readonly IStudentService _studentService;

		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}

		[HttpGet]
		public ActionResult<List<Student>> GetStudents()
		{
			List<Student> students = _studentService.GetAll();

			return Ok(students);
		}

		[HttpGet("{index}")]
		public ActionResult<Student> GetStudentByIndex([FromRoute] string index)
		{
			Student student = _studentService.GetByIndex(index);

			return Ok(student);
		}

		[HttpPut("{index}")]
		public ActionResult UpdateStudentByIndex([FromBody] Student request, [FromRoute] string index)
		{
			_studentService.UpdateByIndex(request, index);

			return Ok("Update Student With index: " + index);
		}

		[HttpPost]
		public ActionResult CreateStudent([FromBody] Student request)
		{
			_studentService.Create(request);

			return Created($"/api/students/{request.Index}", null);
		}

		[HttpDelete("{index}")]
		public ActionResult DeleteStudentByIndex([FromRoute] string index)
		{
            _studentService.Delete(index);

            return NoContent();
		}
	}
}