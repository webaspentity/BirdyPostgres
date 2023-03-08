namespace Birdy.Shared;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }
    public int CategoryId { get; set; }
    public string? ImageSource { get; set; }
    public string? NutritionType { get; set; }
    public string? BirdSize { get; set; }
    public Category? Category { get; set; }
    public List<ProductPrice>? ProductsPrices { get; set; }
    public List<Raiting>? Raitings { get; set; }
    public List<Review>? Reviews { get; set; }
    public List<ProductIngredient>? Ingredients { get; set; }
    public List<CartItem>? CartItems { get; set; }
    public List<ProductOrder>? ProductOrders { get; set; }
    public List<BirdProduct>? BirdProducts { get; set; }
}