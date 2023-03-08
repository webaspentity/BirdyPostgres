using System.ComponentModel.DataAnnotations;
namespace Birdy.Shared;

public class OrderModel
{
    private DateTime date = DateTime.Now.AddDays(1);
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [MaxLength(50, ErrorMessage = "Максимальная длина: 50 символов")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [MaxLength(25, ErrorMessage = "Максимальная длина: 25 символов")]
    public string Telephone { get; set; }

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат электронной почты")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [MaxLength(250, ErrorMessage = "Максимальная длина: 250 символов")]
    public string Address { get; set; }

    public string? Comment { get; set; }

    public DateTime DesiredDeliveryDate { 
        get => date;
        set
        {
            date = value < DateTime.Now.AddDays(1) ? DateTime.Now.AddDays(1) : value;
        } 
    }

    public bool OnlinePayment { get; set; } = false;

    
    public bool IsAgree { get; set; } = false;
}
