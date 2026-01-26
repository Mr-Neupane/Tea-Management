namespace TeaManagement.ViewModels;

public class NewSalesVm
{
    public int ProductId { get; set; }
    public DateTime TxnDate { get; set; } = DateTime.Now.ToUniversalTime();
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal WaterQuantity { get; set; }
    public int FactoryId { get; set; }
}