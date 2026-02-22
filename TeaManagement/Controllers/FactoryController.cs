using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Manager;
using TeaManagement.Repository.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class FactoryController : Controller
{
    private readonly FactoryManager _factoryManager;
    private readonly ApplicationDbContext _context;
    private readonly IToastNotification _toastNotification;
    private readonly IFactoryService _factoryService;
    private readonly IReportRepository _reportRepository;


    public FactoryController(FactoryManager factoryManager, ApplicationDbContext context,
        IToastNotification toastNotification,
        IFactoryService factoryService, IReportRepository reportRepository)
    {
        _factoryManager = factoryManager;
        _context = context;
        _toastNotification = toastNotification;
        _factoryService = factoryService;
        _reportRepository = reportRepository;
    }

    public IActionResult NewFactory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NewFactory(NewFactoryVm vm)
    {
        var existingFactories = await _context.Factories.FirstOrDefaultAsync(f => f.Name == vm.FactoryName);
        if (existingFactories != null)
        {
            _toastNotification.AddErrorToastMessage($"Factory {vm.FactoryName} already exists");
        }
        else
        {
            try
            {
                var factory = new NewFactoryDto
                {
                    Name = vm.FactoryName,
                    Address = vm.FactoryAddress,
                    ContactNumber = vm.FactoryContact,
                    Country = vm.FactoryCountry,
                    LedgerId = 0
                };
                await _factoryManager.AddNewFactory(factory);
                _toastNotification.AddSuccessToastMessage($"{vm.FactoryName} factory created successfully");
                return View();
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage("Error creating factory." + e.Message);
                return View();
            }
        }

        return View();
    }

    public async Task<IActionResult> EditFactory(int saleId)
    {
        var fac = await _factoryService.GetFactoryByIdAsync(saleId);
        return View(fac);
    }

    [HttpGet]
    public async Task<IActionResult> FactoryReport()
    {
        var report = await _reportRepository.GetFactoryReportAsync(null);
        return View(report);
    }


    public async Task<RedirectToActionResult> DeactivateFactory(int factoryId)
    {
        try
        {
            await _factoryManager.DeactivateFactory(factoryId);
            
            _toastNotification.AddSuccessToastMessage("Factory deactivated successfully");
            return RedirectToAction("FactoryReport");
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return RedirectToAction("FactoryReport");
        }
    }
}