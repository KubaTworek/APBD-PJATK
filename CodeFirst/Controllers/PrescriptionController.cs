using CodeFirst.DTO;
using CodeFirst.Model;
using CodeFirst.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptonService;

        public PrescriptionController(IPrescriptionService prescriptonService)
        {
            _prescriptonService = prescriptonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrescriptions()
        {
            var prescriptions = await _prescriptonService.FindAllPrescriptions();
            return Ok(prescriptions);
        }

        [HttpGet("{idPrescrption}")]
        public async Task<IActionResult> GetPrescriptionById([FromRoute] int idPrescrption)
        {
            var prescription = await _prescriptonService.FindPrescriptionById(idPrescrption);
            return Ok(prescription);
        }
    }
}