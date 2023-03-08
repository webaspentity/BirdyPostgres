using System.ComponentModel.DataAnnotations;

namespace Birdy.Client.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат электронной почты")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    public string Password { get; set; } = default!;
}
