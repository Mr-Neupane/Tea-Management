using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Manager.Interface;
using TeaManagement.Providers;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class PurchaseController : Controller
{
    private readonly DropdownProvider _dropdownProvider;
    private readonly IPurchaseManager _purchaseManager;
    private readonly IToastNotification _toastNotification;

    public PurchaseController(DropdownProvider dropdownProvider, IPurchaseManager purchaseManager,
        IToastNotification toastNotification)
    {
        _dropdownProvider = dropdownProvider;
        _purchaseManager = purchaseManager;
        _toastNotification = toastNotification;
    }

    public IActionResult Index()
    {
        var products = _dropdownProvider.GetProductsForPurchase();
        var suppliers = _dropdownProvider.GetSupplierList();
        var vm = new PurchaseVm
        {
            ProductList = new SelectList(products, "Id", "Name"),
            SupplierList = new SelectList(suppliers, "Id", "Name"),
        };
        return View(vm);
    }
[HttpPost]
    public async Task<IActionResult> AddPurchase(PurchaseVm vm)
    {
        try
        {
            var actDtl = vm.PurchaseDetails.Where(x => x.ProductId != 0).ToList();

            if (actDtl.Count>0)
            {
                var dto = new PurchaseDto
                {
                    SupplierId = vm.StakeholderId,
                    BillNo = vm.BillNo,
                    GrossAmount = vm.Amount,
                    Discount = 0,
                    NetAmount = vm.Amount,
                    TxnDate = vm.TxnDate,
                    Details = actDtl.Select(x => new PurchaseDetailsDto
                    {
                        ProductId = x.ProductId,
                        UnitId = x.UnitId,
                        Quantity = x.Quantity,
                        Rate = x.Rate,
                        Discount = 0
                    }).ToList()
                };
                await _purchaseManager.AddPurchase(dto);
                _toastNotification.AddSuccessToastMessage("Purchase added successfully.");
            }
            else
            {
                _toastNotification.AddAlertToastMessage("Please add product first");
            }
           
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return RedirectToAction("Index");
        }
    }
}