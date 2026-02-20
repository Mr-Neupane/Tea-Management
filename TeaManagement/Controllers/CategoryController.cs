using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using TeaManagement.Entities;
using TeaManagement.Repository.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IToastNotification _toastNotification;
    private readonly IReportRepository  _reportRepository;

    public CategoryController(ApplicationDbContext context, IToastNotification toastNotification, IReportRepository reportRepository)
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
    public async Task<IActionResult> Add(CategoryVm vm)
    {
        try
        {
            var existing = _context.Categories.FirstOrDefault(x => x.Name == vm.Name.Trim());
            if (existing == null)
            {
                var cat = new Category
                {
                    Name = vm.Name,
                    ParentCategoryId = null
                };
                await _context.AddAsync(cat);
                await _context.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("Category created successfully.");
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("Category already exists.");
                
            }
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return RedirectToAction("Index");
        }
    }

    public async Task<IActionResult> CategoryReport()
    {
        var report = await _reportRepository.GetCategoryReportAsync();
        return View(report);
    }
}