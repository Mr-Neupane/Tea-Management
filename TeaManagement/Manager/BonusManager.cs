using System.Transactions;
using TeaManagement.Dtos;
using TeaManagement.Interface;

namespace TeaManagement.Manager;

public class BonusManager
{
    private readonly ILedgerService _ledgerService;
    private readonly IBonusService _bonusService;

    public BonusManager(ILedgerService ledgerService, IBonusService bonusService)
    {
        _ledgerService = ledgerService;
        _bonusService = bonusService;
    }

    public async Task CreateBonus(AddBonusDto dto, NewLedgerDto? ledgerdto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            if (dto.IsNewLedger)
            {
                var ledger = await _ledgerService.AddLedgerAsync(ledgerdto);

                var bonus = new AddBonusDto
                {
                    Name = dto.Name,
                    FactoryId = dto.FactoryId,
                    LedgerId = ledger.Id,
                    BonusPerKg = dto.BonusPerKg,
                };
                await _bonusService.AddBonusAsync(bonus);
            }
            else
            {
                var bonus = new AddBonusDto
                {
                    Name = dto.Name,
                    FactoryId = dto.FactoryId,
                    LedgerId = dto.LedgerId,
                    BonusPerKg = dto.BonusPerKg,
                };
                await _bonusService.AddBonusAsync(bonus);
            }

            scope.Complete();
        }
    }
}