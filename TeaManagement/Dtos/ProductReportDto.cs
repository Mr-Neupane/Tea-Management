namespace TeaManagement.Dtos;

public class ProductReportDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string UnitName { get; set; }
    public string CategoryName { get; set; }
    public decimal Rate { get; set; }
}