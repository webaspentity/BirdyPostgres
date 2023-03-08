using System.Text.Json.Serialization;

namespace Birdy.Shared;

[Serializable]
public class Raiting
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [JsonIgnore] public Product? Product { get; set; }
    public int UserId { get; set; }
    [JsonIgnore] public User? User { get; set; }
    public int Value { get; set; }
}
