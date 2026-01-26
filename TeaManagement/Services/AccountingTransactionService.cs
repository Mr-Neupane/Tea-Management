using Microsoft.EntityFrameworkCore;
using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class AccountingTransactionService : IAccountingTransactionService
{
    private readonly ApplicationDbContext _context;

    public AccountingTransactionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AccountingTransaction> RecordAccountingTransactionAsync(AccTransactionDto dto)
    {
        var txn = new AccountingTransaction
        {
            TransactionDate = dto.TxnDate,
            Type = dto.TxnType,
            Amount = dto.Amount,
            TypeId = dto.TypeId,
            IsReversed = false,
            ReversedId = null,
            Details = dto.Details.Select(x => new TransactionDetails
            {
                LedgerId = x.LedgerId,
                DrCr = x.IsDr ? 'D' : 'C',
                DrAmount = x.IsDr ? x.Amount : 0,
                CrAmount = !x.IsDr ? x.Amount : 0,
            }).ToList()
        };
        await _context.AccTransaction.AddAsync(txn);
        await _context.SaveChangesAsync();
        return txn;
    }

    public async Task UpdateTransactionTypeDetailsAsync(int transactionId, int txnTypeId)
    {
        var txn = await _context.AccTransaction.Where(x => x.Id == transactionId).SingleOrDefaultAsync();
        if (txn == null)
        {
            throw new Exception("Transaction not found");
        }
        else
        {
            txn.TypeId = txnTypeId;
             _context.AccTransaction.Update(txn);
        }
    }
}