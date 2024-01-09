namespace HotDrinkDistributor.Domain.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
