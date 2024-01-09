using HotDrinkDistributor.Domain.Models;
using HotDrinkDistributor.Application.Models;

namespace HotDrinkDistributor.Domain.Ports
{
    public interface IHotDrinkService
    {
        RecipeDto GetRecipeDto(int id);

        IEnumerable<RecipeDto> GetAllRecipes();

        void AddRecipe(Recipe recipe);

        void UpdateRecipe(Recipe modifiedRecipe);

        void DeleteRecipe(int recipeId);
    }
}
