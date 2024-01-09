namespace HotDrinkDistributor.Domain.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        //public decimal Price => Ingredients?.Sum(ingredient => (ingredient.Product?.Price ?? 0) * ingredient.Quantity) ?? 0;
    }
}
