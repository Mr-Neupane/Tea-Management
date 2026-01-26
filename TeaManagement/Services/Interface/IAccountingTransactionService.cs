using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface IAccountingTransactionService
{
    public Task<AccountingTransaction> RecordAccountingTransactionAsync(AccTransactionDto dto);
    public Task UpdateTransactionTypeDetailsAsync(int transactionId, int txnTypeId);
}