using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Services.Interface;

public interface IPayableService
{
    public Task<Payable> AddPayableAsync(PayableDto dto);
}