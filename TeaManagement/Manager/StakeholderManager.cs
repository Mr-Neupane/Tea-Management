using System.Transactions;
using TeaManagement.Constraints;
using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Enums;
using TeaManagement.Interface;

namespace TeaManagement.Manager;

public class StakeholderManager
{
    private readonly IStakeholderService _stakeholderService;
    private readonly ILedgerService _ledgerService;

    public StakeholderManager(IStakeholderService stakeholderService, ILedgerService ledgerService)
    {
        _stakeholderService = stakeholderService;
        _ledgerService = ledgerService;
    }

    public async Task<Ledger> RecordStakeholder(StakeholderDto sDto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var ledgerDto = new NewLedgerDto
            {
                LedgerName = sDto.FullName,
                LedgerCode = null,
                SubParentId = sDto.IsSupplier ? ParentLedgerIdConstraints.Creditors : ParentLedgerIdConstraints.Debtors,
                ParentId = null,
                IsParent = false
            };
            var ledger = await _ledgerService.AddLedgerAsync(ledgerDto);

            var stakeholder = new StakeholderDto
            {
                IsSupplier = sDto.IsSupplier,
                FullName = sDto.FullName,
                Email = sDto.Email,
                PhoneNumber = sDto.PhoneNumber,
                Address = sDto.Address,
                LedgerId = ledger.Id,
            };
            await _stakeholderService.RecordStakeholderAsync(stakeholder);
            scope.Complete();
            return ledger;
        }
    }
}