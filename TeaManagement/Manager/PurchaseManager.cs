using System.Transactions;
using TeaManagement.Constraints;
using TeaManagement.Dtos;
using TeaManagement.Interface;
using TeaManagement.Manager.Interface;

namespace TeaManagement.Manager;

public class PurchaseManager : IPurchaseManager
{
    private readonly IPurchaseService _purchaseService;
    private readonly IAccountingTransactionService _accountingTransactionService;
    private readonly ApplicationDbContext _dbContext;

    public PurchaseManager(IPurchaseService purchaseService, IAccountingTransactionService accountingTransactionService,
        ApplicationDbContext dbContext)
    {
        _purchaseService = purchaseService;
        _accountingTransactionService = accountingTransactionService;
        _dbContext = dbContext;
    }

    public async Task AddPurchase(PurchaseDto dto)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var supplierLedgerId =
                _dbContext.Stakeholders.Where(x => x.Id == dto.SupplierId).Select(x => x.LedgerId).Single();
            var purchase = await _purchaseService.AddPurchaseAsync(dto);
            var acctxn = new AccTransactionDto
            {
                TxnDate = dto.TxnDate,
                TxnType = "Purchase",
                TypeId = purchase.Id,
                Amount = dto.NetAmount,
                IsJv = false,
                Details = new List<AccTransactionDetailsDto>()
                {
                    new()
                    {
                        LedgerId = supplierLedgerId,
                        IsDr = false,
                        Amount = dto.NetAmount,
                    },
                    new()
                    {
                        LedgerId = LedgerIdConstraints.Purchase,
                        IsDr = true,
                        Amount = dto.NetAmount,
                    }
                }
            };
            await _accountingTransactionService.RecordAccountingTransactionAsync(acctxn);
            scope.Complete();
        }
    }
}