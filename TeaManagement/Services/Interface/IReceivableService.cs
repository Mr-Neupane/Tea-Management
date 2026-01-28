using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface IReceivableService
{
    public Task<Receivable> RecordReceivableAsync(NewReceivableDto dto);
}