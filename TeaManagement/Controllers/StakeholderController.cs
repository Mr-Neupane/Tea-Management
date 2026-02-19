using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Entities;
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
            var existing = _context.Stakeholders.FirstOrDefault(x => x.FullName == vm.FullName.Trim());

            if (existing != null)
            {
                _toastNotification.AddErrorToastMessage("Supplier with the same name already exists.");
                return View(vm);
            }
            else
            {
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
            }


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
            var exising = _context.Stakeholders.FirstOrDefault(x => x.FullName == vm.FullName.Trim());
            if (exising != null)
            {
                _toastNotification.AddErrorToastMessage("Customer with the same name already exists.");
            }
            else
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
            }


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

    public async Task<IActionResult> DeactivateStakeholder(int stakeholderId, bool isSupplier)
    {
        try
        {
            var stakeholder = await _context.Stakeholders.FindAsync(stakeholderId);
            if (stakeholder != null)
            {
                await _stakeholderManager.DeactivateStakeholder(stakeholder.Id, stakeholder.LedgerId);
                _toastNotification.AddSuccessToastMessage("Stakeholder Deactivated Successfully.");
                return RedirectToAction(isSupplier ? "SupplierReport" : "CustomerReport");
            }
            else
            {
                throw new Exception("Stakeholder not found.");
            }
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return RedirectToAction(isSupplier ? "SupplierReport" : "CustomerReport");
        }
    }
}