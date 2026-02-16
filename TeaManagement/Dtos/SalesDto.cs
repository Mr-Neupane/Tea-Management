namespace TeaManagement.Dtos;

public class SalesDto
{
    public int FactoryId { get; set; }
    public DateTime TxnDate { get; set; }
    public string? BillNo { get; set; }
    public decimal NetAmount { get; set; }
    public List<SalesDetailsDto> Details { get; set; }
}

public class SalesDetailsDto
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal WaterQuantity { get; set; }
    public decimal NetQuantity { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal NetAmount { get; set; }
}