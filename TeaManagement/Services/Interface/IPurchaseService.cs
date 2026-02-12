using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface IPurchaseService
{
    public Task<Purchase> AddPurchaseAsync(PurchaseDto dto);
}