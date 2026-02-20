using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Repository.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class UnitController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IToastNotification _toastNotification;
    private readonly IReportRepository _reportRepository;

    public UnitController(ApplicationDbContext context, IToastNotification toastNotification,
        IReportRepository reportRepository)
    {
        _context = context;
        _toastNotification = toastNotification;
        _reportRepository = reportRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddUnit(UnitVm vm)
    {
        try
        {
            var existing = _context.ProductUnits.FirstOrDefault(x => x.UnitName == vm.UnitName.Trim());
            if (existing == null)
            {
                var unit = new ProductUnit
                {
                    UnitName = vm.UnitName.Trim(),
                    UnitDescription = vm.UnitDescription.Trim(),
                };
                await _context.AddAsync(unit);
                await _context.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("Unit added successfully");
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("Unit with same name already exists");
            }
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> UnitReport()
    {
        var report = await _reportRepository.GetUnitReportAsync();
        return View(report);
    }
}