namespace TeaManagement.Dtos;

public class SalesReportDto
{
    public int Id { get; set; }
    public DateTime SalesDate { get; set; }
    public string SalesNo { get; set; }
    public string? BillNo { get; set; }
    public decimal SalesAmount { get; set; }

    public string FactoryName { get; set; }
}

public class SaleDetailedReportDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal WaterQuantity { get; set; }
    public decimal? BonusAmount { get; set; }
    public DateTime SalesDate { get; set; }
    public string SalesNo { get; set; }
    public string? BillNo { get; set; }
}