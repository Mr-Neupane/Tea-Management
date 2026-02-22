using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Manager;
using TeaManagement.Providers;
using TeaManagement.Repository.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class SaleController : Controller
{
    private readonly SalesTransactionManager _salesTransactionManager;
    private readonly IToastNotification _toastNotification;
    private readonly DropdownProvider _dropdownProvider;
    private readonly IReportRepository _reportRepository;


    public SaleController(SalesTransactionManager salesTransactionManager, IToastNotification toastNotification,
        DropdownProvider dropdownProvider, IReportRepository reportRepository)
    {
        _salesTransactionManager = salesTransactionManager;
        _toastNotification = toastNotification;
        _dropdownProvider = dropdownProvider;
        _reportRepository = reportRepository;
    }

    public IActionResult NewSale()
    {
        var prod = _dropdownProvider.GetProductsForSales();
        var teaClass = _dropdownProvider.GetTeaClass();
        var fac = _dropdownProvider.GetAllFactories();
        var vm = new NewSalesVm
        {
            Factories = new SelectList(fac, "Id", "Name"),
            Products = new SelectList(prod, "Id", "Name"),
            TeaClass = new SelectList(teaClass, "Id", "Name")
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> NewSale(NewSalesVm vm)
    {
        try
        {
            var saleDetails = vm.SalesDetails.Where(x => x.ProductId != 0).ToList();

            if (saleDetails.Count > 0)
            {
                var dto = new SalesDto
                {
                    FactoryId = vm.FactoryId,
                    TxnDate = vm.TxnDate,
                    BillNo = vm.BillNo,
                    NetAmount = vm.Amount,
                    Details = saleDetails.Select(x => new SalesDetailsDto
                    {
                        ProductId = x.ProductId,
                        UnitId = x.UnitId,
                        Quantity = x.Quantity,
                        Rate = x.Price,
                        WaterQuantity = x.WaterQuantity,
                        NetQuantity = x.Quantity - x.WaterQuantity,
                        GrossAmount = x.Quantity * x.Price,
                        NetAmount = (x.Quantity - x.WaterQuantity) * x.Price,
                    }).ToList()
                };

                await _salesTransactionManager.AddSales(dto);
                _toastNotification.AddSuccessToastMessage("Sales added successfully");
            }
            else
            {
                _toastNotification.AddAlertToastMessage("Please add product for sales");
            }

            return RedirectToAction("SalesReport");
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return View(vm);
        }
    }

    public async Task<IActionResult> SalesReport()
    {
        var sales = await _reportRepository.GetSalesListAsync();
        return View(sales);
    }

    public async Task<IActionResult> SaleDetail(int saleId)
    {
        var detail = await _reportRepository.GetSaleDetailedReportAsync(saleId);
        return View(detail);
    }
}