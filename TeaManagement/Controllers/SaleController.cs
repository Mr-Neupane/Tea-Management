using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Manager;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class SaleController : Controller
{
    private readonly SalesTransactionManager  _salesTransactionManager;
    private readonly IToastNotification _toastNotification;


    public SaleController(SalesTransactionManager salesTransactionManager, IToastNotification toastNotification)
    {
        _salesTransactionManager = salesTransactionManager;
        _toastNotification = toastNotification;
    }

    public IActionResult NewSale()
    {
        return View();
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