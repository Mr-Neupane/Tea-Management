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