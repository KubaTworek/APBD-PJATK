using Kolokwium.Model;
using Kolokwium.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kolokwium.Controllers
{
    [Route("api/prescriptions")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrescriptions(string lastName = "")
        {
            var prescriptions = await _prescriptionService.GetAllByLastName(lastName);

            return Ok(prescriptions);
        }

        [HttpPost("{prescriptionId}")]
        public async Task<ActionResult> CreateMedicament([FromBody] MedicamentsRequest request, [FromRoute] int prescriptionId)
        {
            int count = await _prescriptionService.AddMedicaments(request, prescriptionId);
            return Ok(count + "medicaments were added");
        }
    }
}
