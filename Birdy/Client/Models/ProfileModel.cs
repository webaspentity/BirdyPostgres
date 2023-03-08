using System.ComponentModel.DataAnnotations;

namespace Birdy.Client.Models;

public class ProfileModel
{
    public string? Name { get; set; } = default!;
    public string? Telephone { get; set; } = default!;

    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат электронной почты")]
    public string? Email { get; set; } = default!;
    public string? Password { get; set; } = default!;
    public string? Address { get; set; } = default!;
}