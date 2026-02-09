using System.Transactions;
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
            var salesDto = new SalesDto
            {
                ProductId = dto.ProductId,
                Quantity = Math.Round(dto.Quantity, 2),
                Price = dto.Price,
                BillNo = dto.BillNo,
                WaterQuantity = dto.WaterQuantity,
                SalesAmount = dto.SalesAmount,
                FactoryId = dto.FactoryId,
            };
            var sales = await _salesService.AddSalesAsync(salesDto);
            var drLedgerId = await _idProvider.GetFactoryLedgerIdAsync(dto.FactoryId);
            var acctDto = new AccTransactionDto
            {
                TxnDate = dto.TxnDate,
                TxnType = "Sales",
                TypeId = sales.Id,
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
            var accTxn = await _accountingTransactionService.RecordAccountingTransactionAsync(acctDto);
            var stakeholderId = _idProvider.GetStakeholderIdByLedgerId(drLedgerId);
            var rec = new NewReceivableDto
            {
                StakeholderId = stakeholderId,
                TxnDate = dto.TxnDate,
                Amount = dto.SalesAmount,
                TransactionId = accTxn.Id,
            };
            await _receivableService.RecordReceivableAsync(rec);

            scope.Complete();
        }
    }
}