using AnimalAPI.Model;
using AnimalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAPI.Controllers
{
	[ApiController]
	[Route("api/animals")]
	public class AnimalController: ControllerBase
	{
		private readonly IAnimalService _animalService;

		public AnimalController(IAnimalService animalService)
		{
			_animalService = animalService;
		}

        [HttpGet]
        public async Task<IActionResult> GetAnimals(string orderBy = "name")
        {
            var animals = await _animalService.GetAll(orderBy);

            return Ok(animals);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAnimal([FromBody] Animal request)
        {
            if (request == null)
            {
                return BadRequest("Body is required for creating an animal.");
            }

            bool isCreated = await _animalService.Create(request);
            if (isCreated)
            {
                return Ok("Animal was created");
            }
            else
            {
                return BadRequest($"Animal with id {request.IdAnimal} already exists.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnimalById([FromBody] Animal request, [FromRoute] long id)
        {
            bool isUpdated = await _animalService.Update(request, id);
            if (isUpdated)
            {
                return Ok("Updated animal with id: " + id);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnimalById([FromRoute] long id)
        {
            bool isDeleted = await _animalService.Delete(id);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}