using System.Transactions;
using Microsoft.EntityFrameworkCore;
using TeaManagement.Dtos;
using TeaManagement.Interface;

namespace TeaManagement.Manager;

public class FactoryManager
{
    private readonly IFactoryService _factoryService;
    private readonly StakeholderManager _stakeholderManager;
    private readonly IStakeholderService _stakeholderService;
    private readonly ApplicationDbContext _context;


    public FactoryManager(IFactoryService factoryService, StakeholderManager stakeholderManager,
        IStakeholderService stakeholderService, ApplicationDbContext context)
    {
        _factoryService = factoryService;
        _stakeholderManager = stakeholderManager;
        _stakeholderService = stakeholderService;
        _context = context;
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

    public async Task DeactivateFactory(int factoryId)
    {
        var stakeholderId = await (from f in _context.Factories
            join s in _context.Stakeholders on f.LedgerId equals s.LedgerId
            select s.Id).FirstOrDefaultAsync();
        await _stakeholderService.DeactivateStakeholderAsync(stakeholderId);
        await _factoryService.DeactivateFactoryAsync(factoryId);
    }
}