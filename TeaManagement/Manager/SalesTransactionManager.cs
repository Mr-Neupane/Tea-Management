using System.Transactions;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Providers;

namespace TeaManagement.Manager;

public class SalesTransactionManager
{
    private readonly ISalesService _salesService;
    private readonly IAccountingTransactionService _accountingTransactionService;
    private readonly LedgerIdProvider _ledgerIdProvider;

    public SalesTransactionManager(ISalesService salesService,
        IAccountingTransactionService accountingTransactionService, LedgerIdProvider ledgerIdProvider)
    {
        _salesService = salesService;
        _accountingTransactionService = accountingTransactionService;
        _ledgerIdProvider = ledgerIdProvider;
    }

    public async Task AddSales(SalesDto dto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var drLedgerId = await _ledgerIdProvider.GetFactoryLedgerIdAsync(dto.FactoryId);
            var acctxnDto = new AccTransactionDto
            {
                TxnDate = dto.TxnDate,
                TxnType = "Sales",
                TypeId = 0,
                Amount = dto.SalesAmount,
                Details = new List<AccTransactionDetailsDto>
                {
                    new()
                    {
                        LedgerId = drLedgerId,
                        IsDr = true,
                        Amount = dto.SalesAmount,
                    },
                    new()
                    {
                        LedgerId = -13,
                        IsDr = false,
                        Amount = dto.SalesAmount,
                    }
                }
            };
            var accTxn = await _accountingTransactionService.RecordAccountingTransactionAsync(acctxnDto);
            var salesDto = new SalesDto
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Price = dto.Price,
                WaterQuantity = dto.WaterQuantity,
                SalesAmount = dto.SalesAmount,
                FactoryId = dto.FactoryId,
                TransactionId = accTxn.Id,
            };
            var sales = await _salesService.AddSalesAsync(salesDto);
            await _accountingTransactionService.UpdateTransactionTypeDetailsAsync(accTxn.Id, sales.Id);
            scope.Complete();
        }
    }
}