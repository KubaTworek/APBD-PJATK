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
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
		}

        [HttpPut("{index}")]
        public ActionResult UpdateStudentByIndex([FromBody] Student request, [FromRoute] string index)
        {
            bool isUpdated = _studentService.UpdateByIndex(request, index);
            if (isUpdated)
            {
                return Ok("Updated student with index: " + index);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult CreateStudent([FromBody] Student request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Index))
            {
                return BadRequest("Index is required for creating a student.");
            }

            bool isCreated = _studentService.Create(request);
            if (isCreated)
            {
                return CreatedAtAction(nameof(GetStudentByIndex), new { index = request.Index }, request);
            }
            else
            {
                return BadRequest($"Student with index {request.Index} already exists.");
            }
        }

        [HttpDelete("{index}")]
        public ActionResult DeleteStudentByIndex([FromRoute] string index)
        {
            bool isDeleted = _studentService.Delete(index);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}