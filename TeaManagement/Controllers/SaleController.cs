using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class SaleController : Controller
{
    private readonly ISalesService _salesService;
    private readonly IToastNotification _toastNotification;

    public SaleController(ISalesService salesService, IToastNotification toastMessage)
    {
        _salesService = salesService;
        _toastNotification = toastMessage;
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
                Quantity = vm.Quantity,
                Price = vm.Price,
                WaterQuantity = vm.WaterQuantity,
                FactoryId = vm.FactoryId,
            };
            await _salesService.AddSalesAsync(dto);
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