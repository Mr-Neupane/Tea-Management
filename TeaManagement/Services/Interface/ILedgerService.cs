using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface ILedgerService
{
    public Task<Ledger> AddLedgerAsync(NewLedgerDto dto);
    
}

