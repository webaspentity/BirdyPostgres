using Birdy.Shared.Enums;

namespace Birdy.Shared;

[Serializable]
public class User
{
    public int Id { get; set; }
    public LoginData LoginData { get; set; }
    public string Role { get; set; } = UserRole.Anonymous;
    public Profile Profile { get; set; }
    public List<Raiting>? Raitings { get; set; }
    public List<Review>? Reviews { get; set; }
    public List<Order>? Orders { get; set; }
    public Cart Cart { get; set; }
}