using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Enums;
using TeaManagement.Interface;
using TeaManagement.Services.Interface;

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

    public async Task DeactivateLedgerAsync(int ledgerId)
    {
        var ledger = await _context.Ledgers.FindAsync(ledgerId);
        if (ledger != null)
        {
            var totalDr = _context.AccTransactionDetails
                .Where(x => x.LedgerId == ledgerId && x.Status == (int)Status.Active).Sum(x => x.DrAmount);
            var totalCr = _context.AccTransactionDetails
                .Where(x => x.LedgerId == ledgerId && x.Status == (int)Status.Active).Sum(x => x.CrAmount);
            if (totalCr - totalDr == 0)
            {
                ledger.Status = (int)Status.Inactive;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Ledger amount must be zero.");
            }
        }
        else
        {
            throw new Exception("Ledger not found.");
        }
    }

    private string GetLedgerCode(int? subParentId)
    {
        var parentId = _context.Ledgers.Where(x => x.Id == subParentId).Select(x => x.Code).Single();
        var countChild = _context.Ledgers.Count(x => x.SubParentId == subParentId) + 1;
        var ledgerCode = string.Concat(parentId, '.', countChild);
        return ledgerCode;
    }
}