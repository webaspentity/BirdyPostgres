using System.Text.Json.Serialization;

namespace Birdy.Shared;

public class CartItem
{
    public decimal Price { get; set; }
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int Weight { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }

    public int ProductId { get; set; }

    [JsonIgnore]
    public Cart? Cart { get; set; }

    public int CartId { get; set; }
}