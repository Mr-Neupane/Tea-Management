using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeaManagement.ViewModels;

public class AddProductsVm
{
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public string? ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }

    public SelectList Categories { get; set; }
}