using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Model;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequest request)
        {
            int idCreated = await _warehouseService.Create(request);

            return Ok("Created Product_Warehouse with id: " + idCreated);
        }
    }
}

