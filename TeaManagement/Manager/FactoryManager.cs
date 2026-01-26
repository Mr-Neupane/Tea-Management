using System.Threading.Tasks;
using System.Transactions;
using TeaManagement.Dtos;
using TeaManagement.Interface;

namespace TeaManagement.Manager;

public class FactoryManager
{
    private readonly IFactoryService _factoryService;
    private readonly ILedgerService _ledgerService;

    public FactoryManager(IFactoryService factoryService, ILedgerService ledgerService)
    {
        _factoryService = factoryService;
        _ledgerService = ledgerService;
    }

    public async Task AddNewFactory(NewFactoryDto dto, NewLedgerDto ledger)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var newLedger = await _ledgerService.AddLedgerAsync(ledger);
            var fac = new NewFactoryDto
            {
                Name = dto.Name.Trim(),
                Address = dto.Address,
                ContactNumber = dto.ContactNumber,
                Country = dto.Country,
                LedgerId = newLedger.Id
            };
            await _factoryService.AddFactoryAsync(fac);
            scope.Complete();
        }
    }
}