using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeaManagement.Providers;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class PurchaseController : Controller
{
    private readonly DropdownProvider _dropdownProvider;

    public PurchaseController(DropdownProvider dropdownProvider)
    {
        _dropdownProvider = dropdownProvider;
    }

    public IActionResult Index()
    {
        var products = _dropdownProvider.GetAllProducts();
        var vm = new PurchaseVm
        {
            ProductList = new SelectList(products, "Id", "Name"),
        };
        return View(vm);
    }

    public async Task<IActionResult> AddPurchase(PurchaseVm vm)
    {
        return RedirectToAction("Index");
    }
}