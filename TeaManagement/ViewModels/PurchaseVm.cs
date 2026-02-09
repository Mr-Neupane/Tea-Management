using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeaManagement.ViewModels;

public class PurchaseVm
{
    public SelectList ProductList { get; set; }
    public SelectList UnitList { get; set; }
    public List<PurchaseDetailsVm> Purchase { get; set; }
}

public class PurchaseDetailsVm
{
    public int ProductId { get; set; }
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
}