using System.Text.Json.Serialization;

namespace Birdy.Shared;

[Serializable]
public class LoginData
{
    [JsonIgnore]
    public int Id { get; set; }
    public string? Email { get; set; } = default!;
    public string? Password { get; set; } = default!;
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}
