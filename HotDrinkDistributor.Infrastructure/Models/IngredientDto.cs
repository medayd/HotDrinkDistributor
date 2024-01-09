namespace HotDrinkDistributor.Application.Models
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
