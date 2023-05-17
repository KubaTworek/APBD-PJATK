using CodeFirst.DTO;
using CodeFirst.Model;
using CodeFirst.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.FindAllDoctors();
            return Ok(doctors);
        }

        [HttpGet("{idDoctor}")]
        public async Task<IActionResult> GetDoctorById([FromRoute] int idDoctor)
        {
            var doctor = await _doctorService.FindDoctorById(idDoctor);
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorRequest doctorRequest)
        {
            var doctor = await _doctorService.CreateDoctor(doctorRequest);
            string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            string resourceUrl = $"{baseUrl}/api/doctors/{doctor.IdDoctor}";
            return Created(resourceUrl, doctor);
        }

        [HttpPut("{idDoctor}")]
        public async Task<IActionResult> UpdateDoctor([FromBody] DoctorRequest doctorRequest, [FromRoute] int idDoctor)
        {
            var doctor = await _doctorService.UpdateDoctorById(doctorRequest, idDoctor);
            return Ok(doctor);
        }

        [HttpDelete("{idDoctor}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int idDoctor)
        {
            await _doctorService.DeleteDoctorById(idDoctor);
            return NoContent();
        }
    }
}