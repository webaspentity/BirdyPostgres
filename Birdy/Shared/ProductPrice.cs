using System.Text.Json.Serialization;

namespace Birdy.Shared;

public class ProductPrice
{
    public int Id { get; set; }
    public int Weight { get; set; } = 0;
    public decimal Price { get; set; }
    public int InStock { get; set; }
    public int ProductId { get; set; }
    public List<Discount>? Discounts { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }
}