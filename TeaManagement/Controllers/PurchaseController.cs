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
        var suppliers = _dropdownProvider.GetSupplierList();
        var vm = new PurchaseVm
        {
            ProductList = new SelectList(products, "Id", "Name"),
            SupplierList = new SelectList(suppliers, "Id", "Name"),
        };
        return View(vm);
    }

    public async Task<IActionResult> AddPurchase(PurchaseVm vm)
    {
        var actDtl = vm.Purchase.Where(x => x.ProductId != 0).ToList();

        return RedirectToAction("Index");
    }
}