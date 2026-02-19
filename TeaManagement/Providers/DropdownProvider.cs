using Microsoft.AspNetCore.Mvc;
using TeaManagement.Constraints;
using TeaManagement.Dtos;
using TeaManagement.Enums;

namespace TeaManagement.Providers;

public class DropdownProvider : Controller
{
    private readonly ApplicationDbContext _context;

    public DropdownProvider(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<DropdownListDto> GetAllFactories()
    {
        var factories = _context.Factories.Where(x => x.Status == (int)Status.Active).Select(x => new DropdownListDto()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
        return factories;
    }

    public List<DropdownListDto> GetParentLedgers()
    {
        var parentLedgers = (from l in _context.Ledgers
            join c in _context.ChartOfAccounts on l.ParentId equals c.Id
            where l.Status == (int)Status.Active && l.Id != ParentLedgerIdConstraints.BankAccount &&
                  l.Id != ParentLedgerIdConstraints.Debtors && l.Id != ParentLedgerIdConstraints.Creditors &&
                  l.Id != ParentLedgerIdConstraints.SamanBikriAccount &&
                  l.Id != ParentLedgerIdConstraints.SamanKharidAccount
            select new DropdownListDto()
            {
                Id = l.Id,
                Name = string.Concat(l.Name, " [", l.Code, ']')
            }).ToList();
        return parentLedgers;
    }

    public List<DropdownListDto> GetAllBonusLedgers()
    {
        var ledgers = _context.Ledgers.Where(x => x.SubParentId == -3).Select(x => new DropdownListDto()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
        return ledgers;
    }

    public List<DropdownListDto> GetAllProductCategory()
    {
        var cat = _context.Categories.Where(x => x.Status == (int)Status.Active).Select(x => new DropdownListDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
        return cat;
    }

    public List<DropdownListDto> GetProductsForPurchase()
    {
        var prod = _context.Products.Where(x => x.Status == (int)Status.Active).Select(x =>
                new DropdownListDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
            .ToList();
        return prod;
    }

    public List<DropdownListDto> GetProductsForSales()
    {
        var prod = _context.Products.Where(x => x.Status == (int)Status.Active && x.CategoryId == -1).Select(x =>
                new DropdownListDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
            .ToList();
        return prod;
    }

    public List<DropdownListDto> GetStatus()
    {
        var statusList = new List<DropdownListDto>()
        {
            new()
            {
                Id = 1,
                Name = "Active"
            },
            new()
            {
                Id = 2,
                Name = "Reversed"
            }
        };
        return statusList;
    }

    public List<DropdownListDto> GetUnitList()
    {
        var units = _context.ProductUnits.Where(x => x.Status == (int)Status.Active).Select(u =>
            new DropdownListDto()
            {
                Id = u.Id,
                Name = u.UnitName
            }).ToList();
        return units;
    }

    public List<DropdownListDto> GetSupplierList()
    {
        var suppliers = _context.Stakeholders.Where(x => x.StakeholderType == (int)StakeholderType.Supplier).Select(x =>
            new DropdownListDto()
            {
                Id = x.Id,
                Name = x.FullName
            }).ToList();
        return suppliers;
    }

    [HttpGet]
    public JsonResult GetProductUnit(int productId)
    {
        // Example: fetch from DB
        var product = _context.Products
            .Where(p => p.Id == productId)
            .Select(p => new { p.Unit.UnitName, p.Price, p.UnitId })
            .FirstOrDefault();

        return new JsonResult(product);
    }

    public List<DropdownListDto> GetTeaClass()
    {
        var tClass = _context.TeaClass.Where(x => x.Status == (int)Status.Active).Select(x => new DropdownListDto()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
        return tClass;
    }
}