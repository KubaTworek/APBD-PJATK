using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.DAO;
using WarehouseAPI.Model;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [ApiController]
    [Route("api/warehouses2")]
    public class Warehouse2Controller : ControllerBase
    {
        private readonly IWarehouseDAO _warehouseDAO;

        public Warehouse2Controller(IWarehouseDAO warehouseDAO)
        {
            _warehouseDAO = warehouseDAO;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductRequest request)
        {
            bool isCreated = await _warehouseDAO.Procedure(request);
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
