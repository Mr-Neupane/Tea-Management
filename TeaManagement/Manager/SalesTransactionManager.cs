using System.Transactions;
using TeaManagement.Constraints;
using TeaManagement.Dtos;
using TeaManagement.Enums;
using TeaManagement.Interface;
using TeaManagement.Providers;

namespace TeaManagement.Manager;

public class SalesTransactionManager
{
    private readonly ISalesService _salesService;
    private readonly IAccountingTransactionService _accountingTransactionService;
    private readonly IdProvider _idProvider;
    private readonly IReceivableService _receivableService;
    private readonly ApplicationDbContext _context;

    public SalesTransactionManager(ISalesService salesService,
        IAccountingTransactionService accountingTransactionService, IdProvider idProvider,
        IReceivableService receivableService, ApplicationDbContext context)
    {
        _salesService = salesService;
        _accountingTransactionService = accountingTransactionService;
        _idProvider = idProvider;
        _receivableService = receivableService;
        _context = context;
    }

    public async Task AddSales(SalesDto dto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var bonus = GetBonusLedgerId(dto.FactoryId, dto.TxnDate);
            var sales = await _salesService.AddSalesAsync(dto);
            var drLedgerId = await _idProvider.GetFactoryLedgerIdAsync(dto.FactoryId);
            var acctDto = new AccTransactionDto
            {
                TxnDate = dto.TxnDate,
                TxnType = "Sales",
                TypeId = sales.Id,
                Amount = dto.NetAmount,
                Details = new List<AccTransactionDetailsDto>
                {
                    new()
                    {
                        LedgerId = drLedgerId,
                        IsDr = true,
                        Amount = dto.NetAmount,
                    },
                    new()
                    {
                        LedgerId = LedgerIdConstraints.Sales,
                        IsDr = false,
                        Amount = dto.NetAmount,
                    }
                }
            };
            var accTxn = await _accountingTransactionService.RecordAccountingTransactionAsync(acctDto);
            var stakeholderId = _idProvider.GetStakeholderIdByLedgerId(drLedgerId);
            var rec = new NewReceivableDto
            {
                StakeholderId = stakeholderId,
                TxnDate = dto.TxnDate,
                Amount = dto.NetAmount,
                TransactionId = accTxn.Id,
            };
            await _receivableService.RecordReceivableAsync(rec);

            scope.Complete();
        }
    }

    private int? GetBonusLedgerId(int factoryId, DateTime effDate)
    {
        var bonusLedgerId = _context.Bonus.Where(x =>
                x.FactoryId == factoryId && x.Status == (int)Status.Active && x.EffectiveDate <= effDate)
            .Select(x => x.LedgerId)
            .FirstOrDefault();
        return bonusLedgerId;
    }
}