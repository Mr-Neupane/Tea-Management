using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Providers;
using TeaManagement.Repository.Interface;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ApplicationDbContext _context;
    private readonly IToastNotification _toastNotification;
    private readonly DropdownProvider _dropdownProvider;
    private readonly IReportRepository _reportRepository;

    public ProductController(IProductService productService, ApplicationDbContext context,
        IToastNotification toastNotification, DropdownProvider dropdownProvider, IReportRepository reportRepository)
    {
        _productService = productService;
        _context = context;
        _toastNotification = toastNotification;
        _dropdownProvider = dropdownProvider;
        _reportRepository = reportRepository;
    }


    public IActionResult AddProducts()
    {
        var products = _dropdownProvider.GetAllProductCategory();
        var units = _dropdownProvider.GetUnitList();
        var vm = new AddProductsVm
        {
            Categories = new SelectList(products, "Id", "Name"),
            Units = new SelectList(units, "Id", "Name"),
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddProducts(AddProductsVm vm)
    {
        try
        {
            var existingPros = await _context.Products
                .Where(p => p.Name.Trim().ToLower() == vm.ProductName.Trim().ToLower()).FirstOrDefaultAsync();
            if (existingPros != null)
            {
                _toastNotification.AddErrorToastMessage($" {vm.ProductName} already exists");
                return View(vm);
            }
            else
            {
                var dto = new ProductDto
                {
                    Name = vm.ProductName,
                    UnitId = vm.UnitId,
                    CategoryId = vm.CategoryId,
                    Description = vm.ProductDescription,
                    Price = vm.ProductPrice,
                };
                await _productService.CreateProductAsync(dto);
                _toastNotification.AddSuccessToastMessage("Product added successfully!");
            }


            return RedirectToAction("AddProducts");
        }
        catch (Exception e)
        {
            var units = _dropdownProvider.GetUnitList();
            var categories = _dropdownProvider.GetAllProductCategory();
            var newVm = new AddProductsVm
            {
                ProductName = vm.ProductName,
                CategoryId = vm.CategoryId,
                UnitId = vm.UnitId,
                ProductDescription = vm.ProductDescription,
                ProductPrice = vm.ProductPrice,
                Categories = new SelectList(categories, "Id", "Name"),
                Units = new SelectList(units, "Id", "Name"),
            };
            _toastNotification.AddErrorToastMessage("Error adding product." + e.Message);
            return View(newVm);
        }
    }

    public async Task<IActionResult> ProductReport()
    {
        var report = await _reportRepository.GetProductReportAsync(null);
        return View(report);
    }
}