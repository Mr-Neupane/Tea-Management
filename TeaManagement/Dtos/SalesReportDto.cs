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
    public DateTime SalesDate { get; set; }
    public string SalesNo { get; set; }
    public string? BillNo { get; set; }

    public List<DetailsDto> Details { get; set; }
}

public class DetailsDto
{
    public string ProductName { get; set; }
    public string UnitName { get; set; }
    public decimal Quantity { get; set; }
    public decimal Amount { get; set; }
    public decimal Rate { get; set; }
    public decimal WaterQuantity { get; set; }
    public decimal? BonusAmount { get; set; }
    public decimal GrossAmount { get; set; }
}