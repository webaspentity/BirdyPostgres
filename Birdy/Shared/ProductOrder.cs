namespace Birdy.Shared;

public class ProductOrder
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public Order Order { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
}
