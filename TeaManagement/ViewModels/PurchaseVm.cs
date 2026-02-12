using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeaManagement.ViewModels;

public class PurchaseVm
{
    
    public DateTime TxnDate { get; set; }=DateTime.Now;
    public int StakeholderId { get; set; }
    public string? BillNo { get; set; }
    public decimal Amount { get; set; }
    
    public SelectList ProductList { get; set; }
    public SelectList SupplierList { get; set; }
    public List<PurchaseDetailsVm> PurchaseDetails { get; set; }
}

public class PurchaseDetailsVm
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
}