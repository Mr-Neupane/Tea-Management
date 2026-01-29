using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Manager;
using TeaManagement.Providers;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class BonusController : Controller
{
    private readonly BonusManager _bonusManager;
    private readonly IToastNotification _toastNotification;
    private readonly ApplicationDbContext _context;
    private readonly DropdownProvider _dropdownProvider;

    public BonusController(IToastNotification toastNotification, DropdownProvider provider,
        BonusManager bonusManager, ApplicationDbContext context)
    {
        _toastNotification = toastNotification;
        _dropdownProvider = provider;
        _bonusManager = bonusManager;
        _context = context;
    }

    // GET
    public IActionResult AddBonus()
    {
        var factories = _dropdownProvider.GetAllFactories();
        var bonusLedger = _dropdownProvider.GetAllBonusLedgers();
        var vm = new NewBonusVm
        {
            FactorySelectList = new SelectList(factories,
                "Id",
                "Name"),
            BonusLedgerList = new SelectList(bonusLedger,
                "Id", "Name"),
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddBonus(NewBonusVm vm)
    {
        try
        {
            var existingLedger = await _context.Bonus.Where(x => x.Name.Trim() == vm.Name.Trim()).FirstOrDefaultAsync();
            if (existingLedger != null)
            {
                _toastNotification.AddErrorToastMessage("Bonus with that name already exists");
            }
            else
            {
                if (vm.IsNewLedger)
                {
                    var ledger = new NewLedgerDto
                    {
                        LedgerName = vm.Name.Trim(),
                        LedgerCode = null,
                        SubParentId = -3,
                        ParentId = null,
                        IsParent = false
                    };

                    var bonusDto = new AddBonusDto
                    {
                        Name = vm.Name,
                        FactoryId = vm.FactoryId,
                        LedgerId = vm.LedgerId,
                        BonusPerKg = vm.BonusPerKg,
                        IsNewLedger = true,
                    };

                    await _bonusManager.CreateBonus(bonusDto, ledger);
                }
                else
                {
                    var bonusDto = new AddBonusDto
                    {
                        Name = vm.Name,
                        FactoryId = vm.FactoryId,
                        LedgerId = vm.LedgerId,
                        BonusPerKg = vm.BonusPerKg,
                    };
                    await _bonusManager.CreateBonus(bonusDto, null);
                }

                _toastNotification.AddSuccessToastMessage("Bonus added successfully.");
            }

            return Redirect("/");
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage("Something went wrong. Please try again." + e.Message);
            return Redirect("/");
        }
    }
}