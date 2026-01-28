namespace TeaManagement.Dtos;

public class SalesDto
{
    public int ProductId { get; set; }
    public DateTime TxnDate { get; set; } = DateTime.Now.ToUniversalTime();
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal WaterQuantity { get; set; } = 0;
    public decimal SalesAmount { get; set; } = 0;
    public int FactoryId { get; set; }
}