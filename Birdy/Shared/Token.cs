using Birdy.Shared.Enums;

namespace Birdy.Shared;

public class Token
{
    public int Id { get; set; }
    public string Role { get; set; } = UserRole.Anonymous;
    public DateTime ExpiredAt { get; set; } = DateTime.Now.AddHours(1);
    public string Password { get; set; } = string.Empty;
    public string? UserName { get; set; }
    public string? Address { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Telephone { get; set; }

    public int LoginId { get; set; }
    public int ProfileId { get; set; }
    public Cart Cart { get; set; }
}