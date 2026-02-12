using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class PurchaseService : IPurchaseService
{
    private readonly ApplicationDbContext _context;

    public PurchaseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Purchase> AddPurchaseAsync(PurchaseDto dto)
    {
        var number = GetPurchaseNumber();
        var purchase = new Purchase
        {
            SupplierId = dto.SupplierId,
            PurchaseNo = number,
            BillNo = dto.BillNo,
            GrossAmount = dto.GrossAmount,
            Discount = dto.Discount,
            NetAmount = dto.NetAmount,
            TxnDate = dto.TxnDate.ToUniversalTime(),
            PurchaseDetails = dto.Details.Select(x => new PurchaseDetails
            {
                ProductId = x.ProductId,
                UnitId = x.UnitId,
                Quantity = x.Quantity,
                Rate = x.Rate,
                Discount = x.Discount,
            }).ToList()
        };
        await _context.Purchases.AddAsync(purchase);
        await _context.SaveChangesAsync();
        return purchase;
    }

    private string GetPurchaseNumber()
    {
        const string prefix = "PR00000";
        var cn = _context.Purchases.Count() + 1;
        return string.Concat(prefix, cn);
    }
}