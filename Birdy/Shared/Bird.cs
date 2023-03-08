namespace Birdy.Shared;

[Serializable]
public class Bird
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? NutritionType { get; set; }
    public string? Size { get; set; }
    public string? ImageSource { get; set; }
    public List<BirdProduct>? BirdProducts { get; set; }
}