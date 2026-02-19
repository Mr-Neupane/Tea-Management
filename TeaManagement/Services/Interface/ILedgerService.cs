using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Services.Interface;

public interface ILedgerService
{
    public Task<Ledger> AddLedgerAsync(NewLedgerDto dto);
    public Task DeactivateLedgerAsync(int ledgerId);
}