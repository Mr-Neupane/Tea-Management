namespace TeaManagement.Dtos;

public class PurchaseDto
{
    public int SupplierId { get; set; }
    public string? BillNo { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal Discount { get; set; } = 0;
    public decimal NetAmount { get; set; }
    public DateTime TxnDate { get; set; }
    public List<PurchaseDetailsDto> Details { get; set; }
}

public class PurchaseDetailsDto
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Discount { get; set; }
}