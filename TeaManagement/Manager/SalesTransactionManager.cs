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
    private readonly BonusDetailProvider _bonusDetailProvider;

    public SalesTransactionManager(ISalesService salesService,
        IAccountingTransactionService accountingTransactionService, IdProvider idProvider,
        IReceivableService receivableService, BonusDetailProvider bonusDetailProvider)
    {
        _salesService = salesService;
        _accountingTransactionService = accountingTransactionService;
        _idProvider = idProvider;
        _receivableService = receivableService;
        _bonusDetailProvider = bonusDetailProvider;
    }

    public async Task AddSales(SalesDto dto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var sales = await _salesService.AddSalesAsync(dto);

            var bonus = await _bonusDetailProvider.GetBonusDetail(dto.FactoryId, dto.TxnDate);
            var drLedgerId = await _idProvider.GetFactoryLedgerIdAsync(dto.FactoryId);

            if (bonus != null)
            {
                var bonusAmount = bonus.BonusPerKg * dto.Details.Sum(x => x.NetQuantity) ?? 0;

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
                            Amount = (dto.NetAmount + bonusAmount),
                        },
                        new()
                        {
                            LedgerId = LedgerIdConstraints.Sales,
                            IsDr = false,
                            Amount = dto.NetAmount,
                        },
                        new()
                        {
                            LedgerId = bonus.LedgerId ?? 0,
                            IsDr = false,
                            Amount = bonusAmount
                        }
                    }
                };
                var accTxn = await _accountingTransactionService.RecordAccountingTransactionAsync(acctDto);
                var stakeholderId = _idProvider.GetStakeholderIdByLedgerId(drLedgerId);
                var rec = new NewReceivableDto
                {
                    StakeholderId = stakeholderId,
                    TxnDate = dto.TxnDate,
                    Amount = dto.NetAmount + bonusAmount,
                    TransactionId = accTxn.Id,
                };
                await _receivableService.RecordReceivableAsync(rec);
            }
            else
            {
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
            }

            scope.Complete();
        }
    }
}