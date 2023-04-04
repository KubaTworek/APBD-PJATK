using WarehouseAPI.Model;
using WarehouseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseAPI.Controllers
{
	[ApiController]
	[Route("api/warehouses")]
	public class WarehouseController: ControllerBase
	{
		private readonly IWarehouseService _warehouseService;

		public WarehouseController(IWarehouseService warehouseService)
		{
			_warehouseService = warehouseService;
		}

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequest request)
        {
            bool isCreated = await _warehouseService.Create(request);
            if (isCreated)
            {
                return Ok("ProductRequest was created");
            }
            else
            {
                return BadRequest("Exception");
            }
        }
    }
}