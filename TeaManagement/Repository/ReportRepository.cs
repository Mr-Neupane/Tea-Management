using Microsoft.EntityFrameworkCore;
using TeaManagement.Dtos;
using TeaManagement.Enums;
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
                    Rate = p.Price,
                    Status = p.Status,
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
                ContactNo = x.ContactNumber,
                FactoryCountry = x.Country,
                FactoryAddress = x.Address,
                Status = x.Status
            }).ToListAsync();
        return factories;
    }

    public async Task<List<LedgerReportDto>> LedgerReportAsync()
    {
        var res = await (from l in _context.Ledgers
                join pl in _context.Ledgers on l.SubParentId equals pl.Id
                join c in _context.ChartOfAccounts on pl.ParentId equals c.Id
                select new LedgerReportDto
                {
                    LedgerId = l.Id,
                    LedgerName = l.Name,
                    SubParentLedger = pl.Name,
                    CoaLegderName = c.Name,
                    LedgerCode = l.Code,
                    LedgerStatus = l.Status
                }
            ).ToListAsync();
        return res;
    }

    public async Task<List<StakeholderReportDto>> GetStakeholderReportAsync(bool isSupplier, int? status)
    {
        if (isSupplier)
        {
            var res = await _context.Stakeholders
                .Where(x => (x.Status == status || null == status) &&
                            x.StakeholderType == (int)StakeholderType.Supplier).Select(x => new StakeholderReportDto
                {
                    StakeholderId = x.Id,
                    StakeholderName = x.FullName,
                    Email = x.Email,
                    PanNo = 0,
                    ContactNo = x.PhoneNumber,
                    Address = x.Address,
                    IsSupplier = x.StakeholderType == (int)StakeholderType.Supplier,
                    Status = x.Status
                }).ToListAsync();
            return res;
        }
        else
        {
            var res = await _context.Stakeholders
                .Where(x => (x.Status == status || null == status) &&
                            x.StakeholderType == (int)StakeholderType.Customer).Select(x => new StakeholderReportDto
                {
                    StakeholderId = x.Id,
                    StakeholderName = x.FullName,
                    Email = x.Email,
                    PanNo = 0,
                    ContactNo = x.PhoneNumber,
                    Address = x.Address,
                    IsSupplier = x.StakeholderType == (int)StakeholderType.Supplier,
                    Status = x.Status
                }).ToListAsync();
            return res;
        }
    }

    public async Task<List<UnitReportDto>> GetUnitReportAsync()
    {
        var res = await _context.ProductUnits.Where(x => x.Status == (int)Status.Active).Select(x => new UnitReportDto
        {
            Id = x.Id,
            Name = x.UnitName,
            Description = x.UnitDescription,
            Status = x.Status,
        }).ToListAsync();
        return res;
    }

    public async Task<List<CategoryReportDto>> GetCategoryReportAsync()
    {
        var res = await _context.Categories.Where(x => x.Status == (int)Status.Active).Select(x => new CategoryReportDto
        {
            Id = x.Id,
            Name = x.Name,
            Status = x.Status,
        }).ToListAsync();
        return res;
    }

    public async Task<List<SalesReportDto>> GetSalesListAsync()
    {
        var sales = await (from s in _context.Sales
                join f in _context.Factories on s.FactoryId equals f.Id
                where s.Status == (int)Status.Active
                select new SalesReportDto
                {
                    Id = s.Id,
                    SalesDate = s.TxnDate,
                    SalesNo = s.SaleNo,
                    BillNo = s.BillNo,
                    SalesAmount = s.Amount,
                    FactoryName = f.Name,
                }
            ).ToListAsync();


        return sales;
    }

    public async Task<SaleDetailedReportDto> GetSaleDetailedReportAsync(int saleId)
    {
        var validateSaleId = await _context.Sales.FindAsync(saleId);
        if (validateSaleId == null)
        {
            throw new Exception("Sale not found");
        }
        else
        {
            var sales = await (
                from s in _context.Sales
                join sd in _context.SaleDetails on s.Id equals sd.SaleId
                join p in _context.Products on sd.ProductId equals p.Id
                join u in _context.ProductUnits on sd.UnitId equals u.Id
                where sd.Status == (int)Status.Active && s.Id == saleId
                group new { sd, p, u } by new
                {
                    s.Id,
                    s.TxnDate,
                    s.SaleNo,
                    s.BillNo
                }
                into g
                select new SaleDetailedReportDto
                {
                    SalesDate = g.Key.TxnDate,
                    SalesNo = g.Key.SaleNo,
                    BillNo = g.Key.BillNo,
                    Details = g.Select(x => new DetailsDto
                    {
                        ProductName = x.p.Name,
                        UnitName = x.u.UnitName,
                        Quantity = x.sd.Quantity,
                        Amount = x.sd.NetAmount,
                        Rate = x.sd.Rate,
                        WaterQuantity = x.sd.WaterQuantity,
                        BonusAmount = 0,
                        GrossAmount = x.sd.GrossAmount
                    }).ToList()
                }
            ).SingleAsync();
            return sales;
        }
    }
}