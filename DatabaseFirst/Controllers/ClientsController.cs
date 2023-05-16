using DatabaseFirst.Service;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseFirst.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {

        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpDelete("/{idClient}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int idClient)
        {
            await _clientsService.DeleteClient(idClient);
            return NoContent();
        }
    }
}