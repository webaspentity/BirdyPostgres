using Birdy.Shared.Enums;

namespace Birdy.Client.Models;

public class CardModel
{
    public int ProductId { get; set; }
    public string? Href { get; set; }
    public ImageModel? Image { get; set; }
    public int Quantity { get; set; }
    public string? Remark { get; set; }
    public int Weight { get; set; }
    public int InStock { get; set; }
    public string? Description { get; set; }
    public ThemeType Theme { get; set; }
    public string? Title { get; set; }
    public Dictionary<int, decimal> WeightsAndPrices { get; set; }
    public Dictionary<int, int> WeightsAndStocks { get; set; }
    public Dictionary<int, int> WeightsAndDiscounts { get; set; }
}