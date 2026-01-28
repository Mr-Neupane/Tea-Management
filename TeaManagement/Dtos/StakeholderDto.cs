namespace TeaManagement.Dtos;

public class StakeholderDto
{
    public int StakeholderType { get; set; }
    public string FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public int LedgerId { get; set; }
}