using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Dtos;
using TeaManagement.Services;
using TeaManagement.ViewModels;

namespace TeaManagement.Controllers;

public class ProductController : Controller
{
    private readonly ProductService _productService;
    private readonly AppDbContext _context;
    private readonly IToastNotification _toastNotification;

    public ProductController(ProductService productService, AppDbContext context, IToastNotification toastNotification)
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
                .Select(p => p.Name.Trim().ToLower() == vm.ProductName.Trim().ToLower()).FirstOrDefaultAsync();
            if (existingPros)
            {
                _toastNotification.AddAlertToastMessage($" {vm.ProductName} already exists");
            }

            var dto = new ProductDto
            {
                Name = vm.ProductName,
                Description = vm.ProductDescription,
                Price = vm.ProductPrice,
            };
            await _productService.CreateProductAsync(dto);
            _toastNotification.AddSuccessToastMessage("Product added successfully!");

            return View();
        }
        catch (Exception e)
        {
            _toastNotification.AddAlertToastMessage("Error adding product.");
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