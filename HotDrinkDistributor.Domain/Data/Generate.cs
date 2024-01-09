using HotDrinkDistributor.Domain.Models;

namespace HotDrinkDistributor.Domain.Data
{
    public static class Generate
    {
        public static List<Recipe> InitializeData()
        {
            var cafe = new Product { Id = 1, Name = "Café", Price = 1.0m };
            var sucre = new Product { Id = 2, Name = "Sucre", Price = 0.1m };
            var creme = new Product { Id = 3, Name = "Crème", Price = 0.5m };
            var the = new Product { Id = 4, Name = "Thé", Price = 2.0m };
            var eau = new Product { Id = 5, Name = "Eau", Price = 0.2m };
            var chocolat = new Product { Id = 6, Name = "Chocolat", Price = 1.0m };
            var lait = new Product { Id = 7, Name = "Lait", Price = 0.4m };

            var expresso = new Recipe
            {
                Id = 1,
                Name = "Expresso",
                Ingredients =
                [
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 1 }
                ]
            };
            var allonge = new Recipe
            {
                Id = 2,
                Name = "Allongé",
                Ingredients =
                [
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 2 }
                ]
            };
            var cappuccino = new Recipe
            {
                Id = 3,
                Name = "Capuccino",
                Ingredients =
                [
                    new Ingredient { Product = cafe, Quantity = 1 },
                    new Ingredient { Product = chocolat, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 1 },
                    new Ingredient { Product = creme, Quantity = 1 }
                ]
            };
            var chocolate = new Recipe
            {
                Id = 4,
                Name = "Chocolat",
                Ingredients =
                [
                    new Ingredient { Product = chocolat, Quantity = 3 },
                    new Ingredient { Product = lait, Quantity = 2 },
                    new Ingredient { Product = eau, Quantity = 1 },
                    new Ingredient { Product = sucre, Quantity = 1 }
                ]
            };
            var tea = new Recipe
            {
                Id = 5,
                Name = "Tea",
                Ingredients =
                [
                    new Ingredient { Product = the, Quantity = 1 },
                    new Ingredient { Product = eau, Quantity = 2 }
                ]
            };

            return [expresso, allonge, cappuccino, chocolate, tea];
        }
    }
}
