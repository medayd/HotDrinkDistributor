using Microsoft.AspNetCore.Mvc;
using HotDrinkDistributor.Domain.Ports;
using HotDrinkDistributor.Application.Models;
using HotDrinkDistributor.Application.Extensions;

namespace HotDrinkDistributor.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotDrinkController : ControllerBase
    {
        private readonly IHotDrinkService _hotDrinkService;

        public HotDrinkController(IHotDrinkService hotDrinkService) => _hotDrinkService = hotDrinkService;

        [HttpGet("{id}")]
        public IActionResult GetRecipe(int id) => Ok(_hotDrinkService.GetRecipeDto(id));

        [HttpGet]
        public IActionResult GetAllRecipes() => Ok(_hotDrinkService.GetAllRecipes());

        [HttpPost]
        public IActionResult AddRecipe([FromBody] RecipeDto recipe)
        {
            try
            {
                _hotDrinkService.AddRecipe(recipe.ToDomainRecipe());
                return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateRecipe([FromBody] RecipeDto modifiedRecipe)
        {
            try
            {
                _hotDrinkService.UpdateRecipe(modifiedRecipe.ToDomainRecipe());
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipe(int id)
        {
            try
            {
                _hotDrinkService.DeleteRecipe(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
