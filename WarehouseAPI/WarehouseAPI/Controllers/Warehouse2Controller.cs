using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Model;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/warehouses2")]
    public class Warehouse2Controller : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public Warehouse2Controller(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequest request)
        {
            int idCreated = await _warehouseService.CreateProcedure(request);

            return Ok("Created Product_Warehouse with id: " + idCreated);
        }
    }
}
