using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Providers;
using TeaManagement.Repository.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class LedgerController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILedgerService _ledgerService;
    private readonly IToastNotification _toastNotification;
    private readonly IReportRepository _reportRepository;
    private readonly DropdownProvider _dropdownProvider;

    public LedgerController(ApplicationDbContext context, ILedgerService ledgerService,
        IToastNotification toastNotification, DropdownProvider dropdownProvider, IReportRepository reportRepository)
    {
        _context = context;
        _ledgerService = ledgerService;
        _toastNotification = toastNotification;
        _dropdownProvider = dropdownProvider;
        _reportRepository = reportRepository;
    }

    [HttpGet]
    public IActionResult AddParentLedger()
    {
        return View();
    }

    public async Task<IActionResult> AddParentLedger(AddLegderVm vm)
    {
        var ledgerDto = new NewLedgerDto
        {
            LedgerName = vm.LedgerName.Trim(),
            LedgerCode = vm.LedgerCode.Trim(),
            SubParentId = null,
            ParentId = vm.ParentId,
            IsParent = true
        };
        await _ledgerService.AddLedgerAsync(ledgerDto);
        return View();
    }

    [HttpGet]
    public IActionResult CreateLedger()
    {
        var parentLedger = _dropdownProvider.GetParentLedgers();
        var vm = new AddLegderVm
        {
            SubParentIds = new SelectList(parentLedger, "Id", "Name")
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLedger(AddLegderVm vm)
    {
        var existingLedger = _context.Ledgers.Select(x => x.Name == vm.LedgerName.Trim()).FirstOrDefault();
        if (!existingLedger)
        {
            var dto = new NewLedgerDto
            {
                LedgerName = vm.LedgerName.Trim(),
                LedgerCode = vm.LedgerCode != null ? vm.LedgerCode.Trim() : vm.LedgerCode,
                SubParentId = vm.SubParentId,
                ParentId = null,
                IsParent = false
            };
            await _ledgerService.AddLedgerAsync(dto);
            _toastNotification.AddSuccessToastMessage($"{vm.LedgerName} ledger created successfully.");
            return RedirectToAction("LedgerReport");
        }
        else
        {
            _toastNotification.AddErrorToastMessage($"{vm.LedgerName} already exists.");
            return View(vm);
        }
    }

    public async Task<IActionResult> LedgerReport()
    {
        var res = await _reportRepository.LedgerReportAsync();

        return View(res);
    }
}