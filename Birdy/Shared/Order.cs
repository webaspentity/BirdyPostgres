using System.Text.Json.Serialization;

namespace Birdy.Shared;

[Serializable]
public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? Comment { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public int UserId { get; set; }
    public decimal Price { get; set; }
    public bool IsPaid { get; set; }
    public string? Status { get; set; }

    public List<ProductOrder>? ProductOrders { get; set; }
}