using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class SalesService : ISalesService
{
    private readonly ApplicationDbContext _context;

    public SalesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Sale> AddSalesAsync(SalesDto dto)
    {
        var salesNo = GetSalesNo();
        var sales = new Sale
        {
            ProductId = dto.ProductId,
            FactoryId = dto.FactoryId,
            Quantity = dto.Quantity,
            Price = dto.Price,
            SaleNo = salesNo,
            BillNo = dto.BillNo,
            WaterQuantity = dto.WaterQuantity,
            NetQuantity = dto.Quantity - dto.WaterQuantity,
        };
        await _context.Sales.AddAsync(sales);
        await _context.SaveChangesAsync();
        return sales;
    }

    private string GetSalesNo()
    {
        var cn = _context.Sales.Count() + 1;
        const string pref = "SL-0000";
        var salesNo = string.Concat(pref, cn);
        return salesNo;
    }
}