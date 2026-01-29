using TeaManagement.Dtos;
using TeaManagement.Enums;

namespace TeaManagement.Providers;

public class DropdownProvider
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

    public List<DropdownListDto> GetAllProducts()
    {
        var prod = _context.Products.Where(x => x.Status == (int)Status.Active).Select(x => new DropdownListDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToList();
        return prod;
    }
}