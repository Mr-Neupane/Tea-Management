using Microsoft.EntityFrameworkCore;
using TeaManagement.Dtos;
using TeaManagement.Repository.Interface;

namespace TeaManagement.Repository;

public class ReportRepository : IReportRepository
{
    private readonly ApplicationDbContext _context;

    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductReportDto>> GetProductReportAsync(int? status)
    {
        var products = await (from p in _context.Products
                join c in _context.Categories on p.CategoryId equals c.Id
                join u in _context.ProductUnits on p.UnitId equals u.Id
                where (status == null || p.Status == status)
                select new ProductReportDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    UnitName = u.UnitName,
                    CategoryName = c.Name,
                    Rate = p.Price
                })
            .ToListAsync();
        return products;
    }

    public async Task<List<FactoryReportDto>> GetFactoryReportAsync(int? status)
    {
        var factories = await _context.Factories.Where(x => status == null || x.Status == status).Select(x =>
            new FactoryReportDto
            {
                Id = x.Id,
                FactoryName = x.Name,
                FactoryCountry = x.Country,
                FactoryAddress = x.Address,
            }).ToListAsync();
        return factories;
    }
}