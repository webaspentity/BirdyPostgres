using System.Text.Json.Serialization;

namespace Birdy.Shared;

[Serializable]
public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }

    [JsonIgnore]
    public List<Product>? Products { get; set; }
}