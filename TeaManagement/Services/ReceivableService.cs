using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class ReceivableService : IReceivableService
{
    private readonly ApplicationDbContext _context;

    public ReceivableService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Receivable> RecordReceivableAsync(NewReceivableDto dto)
    {
        var recTxn = new Receivable
        {
            StakeholderId = dto.StakeholderId,
            Amount = dto.Amount,
            TxnDate = dto.TxnDate,
            TransactionId = dto.TransactionId,
        };
        await _context.Receivable.AddAsync(recTxn);
        await _context.SaveChangesAsync();
        return recTxn;
    }
}