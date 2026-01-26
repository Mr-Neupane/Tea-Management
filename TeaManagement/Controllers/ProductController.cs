using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Services;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ApplicationDbContext _context;
    private readonly IToastNotification _toastNotification;

    public ProductController(IProductService productService, ApplicationDbContext context,
        IToastNotification toastNotification)
    {
        _productService = productService;
        _context = context;
        _toastNotification = toastNotification;
    }


    public IActionResult AddProducts()
    {
        return View();
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
                _toastNotification.AddAlertToastMessage($" {vm.ProductName} already exists");
                return View(vm);
            }
            else
            {
                var dto = new ProductDto
                {
                    Name = vm.ProductName,
                    Description = vm.ProductDescription,
                    Price = vm.ProductPrice,
                };
                await _productService.CreateProductAsync(dto);
                _toastNotification.AddSuccessToastMessage("Product added successfully!");
            }


            return View();
        }
        catch (Exception e)
        {
            _toastNotification.AddAlertToastMessage("Error adding product." + e.Message);
            return View();
        }
    }

    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _context.Products
            .Select(f => new
            {
                id = f.Id,
                name = f.Name
            })
            .ToList();

        return Json(products);
    }
}