namespace TeaManagement.Dtos;

public class StakeholderDto
{
    public string FullName { get; set; }
    
    public bool IsSupplier { get; set; } = false;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public int LedgerId { get; set; }
}