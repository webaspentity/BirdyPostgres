using System.Text.Json.Serialization;

namespace Birdy.Shared;

public class Discount
{
    public int Id { get; set; }
    public int ProductPriceId { get; set; }
    public int Value { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [JsonIgnore]
    public ProductPrice? ProductPrice { get; set; }
}