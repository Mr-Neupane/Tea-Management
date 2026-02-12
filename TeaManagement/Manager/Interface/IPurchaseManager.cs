using TeaManagement.Dtos;

namespace TeaManagement.Manager.Interface;

public interface IPurchaseManager
{
    public Task AddPurchase(PurchaseDto dto);
}