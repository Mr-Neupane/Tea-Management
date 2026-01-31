using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateProductAsync(ProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            UnitId = dto.UnitId,
            Price = dto.Price,
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
}