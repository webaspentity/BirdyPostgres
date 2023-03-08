namespace Birdy.Shared;

public class Ingredient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageSource { get; set; }
    public List<ProductIngredient> Products { get; set; } = new();
}