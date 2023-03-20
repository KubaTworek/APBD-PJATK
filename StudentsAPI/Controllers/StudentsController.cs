using Microsoft.AspNetCore.Mvc;

namespace StudentsAPI.Controllers
{
	[ApiController]
	[Route("api/students")]
	public class StudentsController: ControllerBase
	{
		private readonly ILogger<StudentsController> _logger;

		public StudentsController(ILogger<StudentsController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public String GetStudents()
		{
			return "Get Students";
		}

		[HttpGet("{index}")]
		public String GetStudentByIndex([FromRoute] string index)
		{
			return "Get Student By " + index;
		}

		[HttpPut("{index}")]
		public String GetStudentByIndex([FromRoute] string index)
		{
			return "Update Student By " + index;
		}

		[HttpPost]
		public String CreateStudent()
		{
			return "Create Student";
		}

		[HttpDelete("{index}")]
		public String DeleteStudentByIndex([FromRoute] string index)
		{
			return "Delete Student By " + index;
		}
	}
}