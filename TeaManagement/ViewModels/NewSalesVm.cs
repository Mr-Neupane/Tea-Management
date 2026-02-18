using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeaManagement.ViewModels;

public class NewSalesVm
{
    public DateTime TxnDate { get; set; } =DateTime.Now.ToUniversalTime();
    public int FactoryId { get; set; }
    public string BillNo { get; set; }
    public decimal Amount { get; set; }
    public SelectList Factories { get; set; }
    public SelectList Products { get; set; }
    public SelectList TeaClass { get; set; }
    public List<SalesDetailsVm> SalesDetails { get; set; }
}

public class SalesDetailsVm
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public int UnitId { get; set; }
    public decimal ClassId { get; set; }
    public decimal Price { get; set; }
    public decimal WaterQuantity { get; set; }
}