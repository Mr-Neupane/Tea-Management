using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Services.Interface;

namespace TeaManagement.Services;

public class PayableService : IPayableService
{
    private readonly ApplicationDbContext _context;

    public PayableService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Payable> AddPayableAsync(PayableDto dto)
    {
        var payable = new Payable
        {
            StakeholderId = dto.StakeholderId,
            Amount = dto.Amount,
            TxnDate = dto.PayableDate.ToUniversalTime()
        };

        await _context.Payables.AddAsync(payable);
        return payable;
    }
}