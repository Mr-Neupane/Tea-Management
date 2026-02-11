using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class PurchaseService :IPurchaseService
{
    public Task<Purchase> AddPurchaseAsync(Purchase dto)
    {
       

        var purchase = new Purchase
        {
            
            SupplierId = 0,
            PurchaseNo = null,
            BillNo = null,
            GrossAmount = 0,
            Discount = 0,
            NetAmount = 0,
            TxnDate = default,
            PurchaseDetails = null
        };
        
        throw new NotImplementedException();
    }
}