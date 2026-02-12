using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Manager;
using TeaManagement.Providers;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class SaleController : Controller
{
    private readonly SalesTransactionManager _salesTransactionManager;
    private readonly IToastNotification _toastNotification;
    private readonly DropdownProvider _dropdownProvider;


    public SaleController(SalesTransactionManager salesTransactionManager, IToastNotification toastNotification,
        DropdownProvider dropdownProvider)
    {
        _salesTransactionManager = salesTransactionManager;
        _toastNotification = toastNotification;
        _dropdownProvider = dropdownProvider;
    }

    public IActionResult NewSale()
    {
        var prod = _dropdownProvider.GetProductsForSales();
        var fac = _dropdownProvider.GetAllFactories();
        var vm = new NewSalesVm
        {
            Factories = new SelectList(fac, "Id", "Name"),
            Products = new SelectList(prod, "Id", "Name"),
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> NewSale(NewSalesVm vm)
    {
        try
        {
            var dto = new SalesDto
            {
                ProductId = vm.ProductId,
                TxnDate = vm.TxnDate,
                Quantity = vm.Quantity,
                Price = vm.Price,
                BillNo = vm.BillNo,
                WaterQuantity = vm.WaterQuantity,
                SalesAmount = Math.Round((vm.Quantity - vm.WaterQuantity) * vm.Price, 2),
                FactoryId = vm.FactoryId
            };

            await _salesTransactionManager.AddSales(dto);
            _toastNotification.AddSuccessToastMessage("Sales added successfully");
            return View();
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return View(vm);
        }
    }
}