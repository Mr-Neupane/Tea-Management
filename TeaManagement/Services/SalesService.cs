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
            FactoryId = dto.FactoryId,
            TxnDate = dto.TxnDate.ToUniversalTime(),
            SaleNo = salesNo,
            Amount = dto.NetAmount,
            BillNo = dto.BillNo,
            SalesDetails = dto.Details.Select(x => new SaleDetails
            {
                ProductId = x.ProductId,
                UnitId = x.UnitId,
                Quantity = x.Quantity,
                Rate = x.Rate,
                WaterQuantity = x.WaterQuantity,
                NetQuantity = x.NetQuantity,
                GrossAmount = x.GrossAmount,
                NetAmount = x.NetAmount,
            }).ToList()
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