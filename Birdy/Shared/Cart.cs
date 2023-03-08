using System.Text.Json.Serialization;

namespace Birdy.Shared;

public class Cart
{
    public int Id { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    public int UserId { get; set; }
    public decimal TotalCost { get; set; }
    public List<CartItem>? Items { get; set; }
}