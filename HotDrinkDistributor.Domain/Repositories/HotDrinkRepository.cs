using HotDrinkDistributor.Domain.Data;
using HotDrinkDistributor.Domain.Ports;
using HotDrinkDistributor.Domain.Models;

namespace HotDrinkDistributor.Domain.Repositories
{
    public class HotDrinkRepository : IHotDrinkRepository
    {
        private List<Recipe> _recipes;

        public HotDrinkRepository()
        {
            _recipes = Generate.InitializeData();
        }

        public Recipe GetRecipe(int id)=> _recipes.FirstOrDefault(drink => drink.Id == id);

        public IEnumerable<Recipe> GetAllRecipes() => _recipes;

        public void AddRecipe(Recipe recipe) => _recipes.Add(recipe);

        public void UpdateRecipe(Recipe modifiedRecipe)
        {
            var recipeToModify = GetRecipe(modifiedRecipe.Id);
            recipeToModify.Name = modifiedRecipe.Name;
            recipeToModify.Ingredients = modifiedRecipe.Ingredients;
        }

        public void DeleteRecipe(Recipe recipeToRemove) => _recipes.Remove(recipeToRemove);
    }
}
