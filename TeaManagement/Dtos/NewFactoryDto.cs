namespace TeaManagement.Dtos;

public class NewFactoryDto
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? ContactNumber { get; set; }
    public string Country { get; set; }
    public int LedgerId { get; set; }
}