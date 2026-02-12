using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Manager;
using TeaManagement.Repository.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class StakeholderController : Controller
{
    private readonly StakeholderManager _stakeholderManager;
    private readonly IToastNotification _toastNotification;
    private readonly ApplicationDbContext _context;
    private readonly IReportRepository _reportRepository;


    public StakeholderController(StakeholderManager stakeholderManager, IToastNotification toastNotification,
        ApplicationDbContext context, IReportRepository reportRepository)
    {
        _stakeholderManager = stakeholderManager;
        _toastNotification = toastNotification;
        _context = context;
        _reportRepository = reportRepository;
    }

    public IActionResult AddSupplier()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSupplier(AddStakeholderVm vm)
    {
        try
        {
            var existing = await _context.Stakeholders.Select(x => x.FullName == vm.FullName.Trim())
                .FirstOrDefaultAsync();
            if (existing)
            {
                _toastNotification.AddErrorToastMessage("Supplier with the same name already exists.");
                return View(vm);
            }

            var dto = new StakeholderDto
            {
                FullName = vm.FullName,
                IsSupplier = true,
                Email = vm.Email,
                PhoneNumber = vm.ContactNumber,
                Address = vm.Address,
            };
            await _stakeholderManager.RecordStakeholder(dto);
            _toastNotification.AddSuccessToastMessage("Supplier added successfully.");
            return View();
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return View(vm);
        }
    }

    public async Task<IActionResult> SupplierReport(int? status)
    {
        var report = await _reportRepository.GetStakeholderReportAsync(true, status);
        return View(report);
    }

    public IActionResult AddCustomer()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer(AddStakeholderVm vm)
    {
        try
        {
            var dto = new StakeholderDto
            {
                FullName = vm.FullName,
                IsSupplier = false,
                Email = vm.Email,
                PhoneNumber = vm.ContactNumber,
                Address = vm.Address,
            };
            await _stakeholderManager.RecordStakeholder(dto);
            _toastNotification.AddSuccessToastMessage("Customer added successfully.");
            return View();
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return View(vm);
        }
    }


    public async Task<IActionResult> CustomerReport(int? status)
    {
        var report = await _reportRepository.GetStakeholderReportAsync(false, status);
        return View(report);
    }
}