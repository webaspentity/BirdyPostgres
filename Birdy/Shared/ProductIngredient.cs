using System.Text.Json.Serialization;

namespace Birdy.Shared;

public class ProductIngredient
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }

    public int IngredientId { get; set; }

    [JsonIgnore]
    public Ingredient? Ingredient { get; set; }
}