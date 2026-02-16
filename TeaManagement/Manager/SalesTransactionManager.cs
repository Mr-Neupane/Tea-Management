using System.Transactions;
using TeaManagement.Constraints;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Providers;

namespace TeaManagement.Manager;

public class SalesTransactionManager
{
    private readonly ISalesService _salesService;
    private readonly IAccountingTransactionService _accountingTransactionService;
    private readonly IdProvider _idProvider;
    private readonly IReceivableService _receivableService;

    public SalesTransactionManager(ISalesService salesService,
        IAccountingTransactionService accountingTransactionService, IdProvider idProvider,
        IReceivableService receivableService)
    {
        _salesService = salesService;
        _accountingTransactionService = accountingTransactionService;
        _idProvider = idProvider;
        _receivableService = receivableService;
    }

    public async Task AddSales(SalesDto dto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
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
}