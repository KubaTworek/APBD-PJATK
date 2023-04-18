using Kolokwium.Model;
using Kolokwium.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers
{
    [Route("api")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("/prescriptions")]
        public async Task<IActionResult> GetPrescrptions(string lastName = "")
        {
            var prescriptions = await _prescriptionService.GetAllByLastName(lastName);

            return Ok(prescriptions);
        }

        [HttpPost("/medicaments")]
        public async Task<ActionResult> CreateMedicament([FromBody] MedicamentsRequest request)
        {
            int count = await _prescriptionService.AddMedicaments(request);
            return Ok(count + "medicaments were added");
        }
    }
}
