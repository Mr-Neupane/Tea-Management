namespace TeaManagement.Dtos;

public class FactoryReportDto
{
    public int Id { get; set; }
    public string FactoryName { get; set; }
    public string? ContactNo { get; set; }
    public string FactoryCountry { get; set; }
    public string? FactoryAddress { get; set; }
    public int Status { get; set; }
}