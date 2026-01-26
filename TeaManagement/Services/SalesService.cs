using System.Threading.Tasks;
using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class SalesService : ISalesService
{
    private readonly AppDbContext _context;

    public SalesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Sale> AddSalesAsync(SalesDto dto)
    {
        var sales = new Sale
        {
            ProductId = dto.ProductId,
            FactoryId = dto.FactoryId,
            Quantity = dto.Quantity,
            Price = dto.Price,
            WaterQuantity = dto.WaterQuantity,
            NetQuantity = dto.Quantity - dto.WaterQuantity,
        };
        await _context.Sales.AddAsync(sales);
        return sales;
    }
}