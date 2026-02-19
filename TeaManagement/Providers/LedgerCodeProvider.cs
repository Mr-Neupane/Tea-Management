using Microsoft.AspNetCore.Mvc;

namespace TeaManagement.Providers;

public class LedgerCodeProvider : Controller
{
    private readonly ApplicationDbContext _context;

    public LedgerCodeProvider(ApplicationDbContext context)
    {
        _context = context;
    }

    public string GetLedgerCode(int parentId)
    {
        var count = _context.Ledgers.Count(x => x.SubParentId == parentId) + 1;
        var code = _context.Ledgers.Where(x => x.Id == parentId).Select(x => x.Code).FirstOrDefault();
        var ledgerCode = string.Concat(code, '.', count);
        return ledgerCode;
    }
}