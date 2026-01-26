using Microsoft.EntityFrameworkCore;
using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class LedgerService : ILedgerService
{
    private readonly ApplicationDbContext _context;

    public LedgerService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Ledger> AddLedgerAsync(NewLedgerDto dto)
    {
        var code = dto.IsParent ? dto.LedgerCode : GetLedgerCode(dto.SubParentId);
        var ledger = new Ledger
        {
            Name = dto.LedgerName.Trim(),
            Code = code,
            ParentId = dto.ParentId,
            SubParentId = dto.SubParentId,
        };
        await _context.Ledgers.AddAsync(ledger);
        await _context.SaveChangesAsync();
        return ledger;
    }

    public async Task<List<LedgerReportDto>> LedgerReportAsync()
    {
        var res = await (from l in _context.Ledgers
                join pl in _context.Ledgers on l.SubParentId equals pl.Id
                join c in _context.ChartOfAccounts on pl.ParentId equals c.Id
                select new LedgerReportDto
                {
                    LedgerName = l.Name,
                    SubParentLedger = pl.Name,
                    CoaLegderName = c.Name,
                    LedgerCode = l.Code,
                    LedgerStatus = l.Status
                }
            ).ToListAsync();
        return res;
    }

    private string GetLedgerCode(int? subParentId)
    {
        var parentId =  _context.Ledgers.Where(x => x.Id == subParentId).Select(x => x.Code).Single();
        var countChild = _context.Ledgers.Count(x => x.SubParentId == subParentId) + 1;
        var ledgerCode = string.Concat(parentId, '.', countChild);
        return ledgerCode;
    }
}