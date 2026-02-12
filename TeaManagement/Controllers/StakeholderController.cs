using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Manager;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class StakeholderController : Controller
{
    private readonly StakeholderManager _stakeholderManager;
    private readonly IToastNotification _toastNotification;
    private readonly ApplicationDbContext _context;


    public StakeholderController(StakeholderManager stakeholderManager, IToastNotification toastNotification,
        ApplicationDbContext context)
    {
        _stakeholderManager = stakeholderManager;
        _toastNotification = toastNotification;
        _context = context;
    }

    public IActionResult AddSupplier()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddSupplier(AddSupplierVm vm)
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
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return View(vm);
        }

        return View();
    }
}