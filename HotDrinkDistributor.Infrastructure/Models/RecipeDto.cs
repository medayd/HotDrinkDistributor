namespace HotDrinkDistributor.Application.Models
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
