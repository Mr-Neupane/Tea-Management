namespace TeaManagement.Dtos;

public class StakeholderReportDto
{
    public int StakeholderId { get; set; }
    public string StakeholderName { get; set; }
    public string? Email { get; set; }
    public int? PanNo { get; set; }
    public string? ContactNo { get; set; }
    public string? Address { get; set; }
}