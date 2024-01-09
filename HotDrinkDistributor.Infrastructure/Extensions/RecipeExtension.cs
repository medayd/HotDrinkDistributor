using HotDrinkDistributor.Domain.Models;
using HotDrinkDistributor.Application.Models;

namespace HotDrinkDistributor.Application.Extensions
{
    public static class RecipeExtensions
    {
        public static Recipe ToDomainRecipe(this RecipeDto recipeInfra) =>
            new()
            {
                Id = recipeInfra.Id,
                Name = recipeInfra.RecipeName,
                Ingredients = recipeInfra.Ingredients.Select(ToDomainIngredient).ToList(),
            };

        private static Ingredient ToDomainIngredient(this IngredientDto ingredientInfra) =>
           new()
           {
               Product = new Product
               {
                   Id = ingredientInfra.Product.Id,
                   Name = ingredientInfra.Product.Name,
                   Price = ingredientInfra.Product.Price
               },
               Quantity = ingredientInfra.Quantity
           };

        public static RecipeDto ToDtoRecipe(this Recipe recipe) =>
    new()
    {
        Id = recipe.Id,
        RecipeName = recipe.Name,
        Ingredients = recipe.Ingredients?.Select(ToDtoIngredient).ToList()
    };

        private static IngredientDto ToDtoIngredient(this Ingredient ingredient) =>
           new()
           {
               Product = ingredient.Product != null ? new ProductDto
               {
                   Name = ingredient.Product.Name,
                   Price = ingredient.Product.Price,
                   Id = ingredient.Product.Id
               } : new ProductDto(),
               Quantity = ingredient.Quantity
           };

    }
}
