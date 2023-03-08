namespace Birdy.Shared;

public class BirdProduct
{
    public int Id { get; set; }
    public Bird? Bird { get; set; }
    public int BirdId { get; set; }
    public Product? Product { get; set; }
    public int ProductId { get; set; }
}
