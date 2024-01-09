using HotDrinkDistributor.Domain.Models;

namespace HotDrinkDistributor.Domain.Ports
{
    public interface IHotDrinkRepository
    {
        Recipe GetRecipe(int id);

        IEnumerable<Recipe> GetAllRecipes();

        void AddRecipe(Recipe recipe);

        void UpdateRecipe(Recipe modifiedRecipe);     

        void DeleteRecipe(Recipe recipeToRemove);
    }
}
