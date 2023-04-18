using Kolokwium.Model;
using Kolokwium.Service;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
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

        [HttpPost("{prescriptionId}/medicaments")]
        public async Task<ActionResult> CreateMedicament([FromBody] MedicamentsRequest request, [FromRoute] int prescriptionId)
        {
            int count = await _prescriptionService.AddMedicaments(request.Medicaments, prescriptionId);
            return Ok(count + " medicaments were added");
        }
    }
}
