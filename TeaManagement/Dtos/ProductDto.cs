namespace TeaManagement.Dtos;

public class ProductDto
{
    public string Name { get; set; }
    public int UnitId { get; set; }
    public int CategoryId { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; } = 0;
}