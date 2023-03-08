using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Birdy.Shared;

[Serializable]
public class Profile
{
    [JsonIgnore]
    public int Id { get; set; }
    [MaxLength(60, ErrorMessage = "Максимальная длина: 60 символов")]
    public string? UserName { get; set; } = default!;
    [MaxLength(150, ErrorMessage = "Максимальная длина: 150 символов")]
    public string? UserAddress { get; set; } = default!;
    [MaxLength(18, ErrorMessage = "Формат номера: +7 (ххх) ххх-хх-хх")]
    public string? UserTelephone { get; set; } = default!;
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}