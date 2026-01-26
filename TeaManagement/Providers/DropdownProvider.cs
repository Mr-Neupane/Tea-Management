using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeaManagement.Enums;

namespace TeaManagement.Providers;

public class DropdownProvider
{
    private readonly AppDbContext _context;

    public DropdownProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<SelectListItem>> FactorySelectList()
    {
        var factories = await _context.Factories.Where(x => x.Status == (int)Status.Active).ToListAsync();
        return factories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
    }

    public async Task<List<SelectListItem>> BonusLedgerList()
    {
        var ledgers = await _context.Ledgers.Where(x => x.SubParentId == -3).ToListAsync();
        return ledgers.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
    }
}