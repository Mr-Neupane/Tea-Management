using System;
using System.Linq;
using System.Threading.Tasks;
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
                var ledger = new NewLedgerDto
                {
                    LedgerName = vm.FactoryName.Trim(),
                    LedgerCode = null,
                    SubParentId = -8,
                    ParentId = null
                };


                var factory = new NewFactoryDto
                {
                    Name = vm.FactoryName,
                    Address = vm.FactoryAddress,
                    ContactNumber = vm.FactoryContact,
                    Country = vm.FactoryCountry,
                    LedgerId = 0
                };
                await _factoryManager.AddNewFactory(factory, ledger);
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

    public async Task<IActionResult> EditFactory(int id)
    {
        var fac = await _factoryService.GetFactoryByIdAsync(id);
        return View(fac);
    }

    [HttpGet]
    public async Task<IActionResult> FactoryReport()
    {
        var report = await _reportRepository.GetFactoryReportAsync(null);
        return View(report);
    }
}