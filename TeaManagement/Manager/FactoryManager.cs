using System.Transactions;
using TeaManagement.Dtos;
using TeaManagement.Interface;

namespace TeaManagement.Manager;

public class FactoryManager
{
    private readonly IFactoryService _factoryService;
    private readonly StakeholderManager _stakeholderManager;


    public FactoryManager(IFactoryService factoryService, StakeholderManager stakeholderManager)
    {
        _factoryService = factoryService;
        _stakeholderManager = stakeholderManager;
    }

    public async Task AddNewFactory(NewFactoryDto dto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var stakeholder = new StakeholderDto
            {
                IsSupplier = false,
                FullName = dto.Name.Trim(),
                Email = null,
                PhoneNumber = dto.ContactNumber,
                Address = dto.Address,
            };
            var ledger = await _stakeholderManager.RecordStakeholder(stakeholder);

            var fac = new NewFactoryDto
            {
                Name = dto.Name.Trim(),
                Address = dto.Address,
                ContactNumber = dto.ContactNumber,
                Country = dto.Country,
                LedgerId = ledger.Id
            };
            await _factoryService.AddFactoryAsync(fac);


            scope.Complete();
        }
    }
}