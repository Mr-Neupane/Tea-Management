using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface IPurchaseService
{
    public Task<Purchase> AddPurchaseAsync(Purchase purchase);
}