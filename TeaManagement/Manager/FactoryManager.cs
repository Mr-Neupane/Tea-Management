using System.Transactions;
using TeaManagement.Dtos;
using TeaManagement.Enums;
using TeaManagement.Interface;

namespace TeaManagement.Manager;

public class FactoryManager
{
    private readonly IFactoryService _factoryService;
    private readonly ILedgerService _ledgerService;
    private readonly IStakeholderService _stakeholderService;


    public FactoryManager(IFactoryService factoryService, ILedgerService ledgerService,
        IStakeholderService stakeholderService)
    {
        _factoryService = factoryService;
        _ledgerService = ledgerService;
        _stakeholderService = stakeholderService;
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

            var stakeholder = new StakeholderDto
            {
                StakeholderType = (int)StakeholderType.Customer,
                FullName = dto.Name.Trim(),
                Email = null,
                PhoneNumber = dto.ContactNumber,
                Address = dto.Address,
                LedgerId = newLedger.Id,
            };
            await _stakeholderService.RecordStakeholderAsync(stakeholder);

            scope.Complete();
        }
    }
}