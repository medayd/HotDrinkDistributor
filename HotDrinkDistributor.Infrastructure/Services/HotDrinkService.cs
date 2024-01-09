using HotDrinkDistributor.Domain.Ports;
using HotDrinkDistributor.Domain.Models;
using HotDrinkDistributor.Application.Models;
using HotDrinkDistributor.Application.Extensions;
using HotDrinkDistributor.Domain.Config;
using Microsoft.Extensions.Options;

namespace HotDrinkDistributor.Application.Services
{
    public class HotDrinkService : IHotDrinkService
    {
        private readonly IHotDrinkRepository _hotDrinkRepository;
        private readonly MargeConfig _config;

        public HotDrinkService(IHotDrinkRepository hotDrinkRepository, IOptions<MargeConfig> margeConfiguration)
        {
            _hotDrinkRepository = hotDrinkRepository;
            _config = margeConfiguration.Value;
        }
        private Recipe GetRecipe(int id) 
            => _hotDrinkRepository.GetRecipe(id) 
                ?? throw new InvalidOperationException("Recipe Not Found");
        
        //Normally, IDs are auto generated in the database, with one to many relations, no need to read all data
        //But to make this work easily and fast (mock), I made it this way
        public void AddRecipe(Recipe recipe)
        {
            var allRecipes = GetAllRecipes();
            if (allRecipes.Any(existingRecipe => existingRecipe.RecipeName == recipe.Name))
            {
                throw new InvalidOperationException("Recipe name must be unique.");
            }
            if (recipe.Ingredients == null || recipe.Ingredients.Count == 0)
            {
                throw new InvalidOperationException("A recipe must have ingredients.");
            }

            recipe.Id = allRecipes.Any() ? allRecipes.Select(r => r.Id).Max() + 1 : 1;

            _hotDrinkRepository.AddRecipe(recipe);
        }

        public void UpdateRecipe(Recipe modifiedRecipe)
        {
            Recipe recipeToModify = GetRecipe(modifiedRecipe.Id);
           
            recipeToModify.Name = modifiedRecipe.Name;
            recipeToModify.Ingredients = modifiedRecipe.Ingredients;
            _hotDrinkRepository.UpdateRecipe(recipeToModify);
        }

        public void DeleteRecipe(int recipeId)
        {
            Recipe recipeToRemove = GetRecipe(recipeId);
         
            _hotDrinkRepository.DeleteRecipe(recipeToRemove);
        }

        public RecipeDto GetRecipeDto(int id)
        {
            Recipe recipe = _hotDrinkRepository.GetRecipe(id);
            if (recipe == null)
                throw new InvalidOperationException("Recipe Not Found");

            RecipeDto recipeDto = recipe.ToDtoRecipe();
            CalculateRecipePrice(recipeDto);
            return CalculateRecipePrice(recipeDto); 
        }

        public IEnumerable<RecipeDto> GetAllRecipes()
        {
            return _hotDrinkRepository.GetAllRecipes().Select(RecipeExtensions.ToDtoRecipe).Select(CalculateRecipePrice);
        }

        private RecipeDto CalculateRecipePrice(RecipeDto recipe)
        {
            recipe.TotalPrice = recipe.Ingredients?.Sum(ingredient => (ingredient.Product?.Price ?? 0) * ingredient.Quantity * _config.Marge) ?? 0;
            return recipe;
        }

    }
}
