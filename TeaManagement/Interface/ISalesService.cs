using System.Threading.Tasks;
using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface ISalesService
{
    public Task<Sale> AddSalesAsync( SalesDto dto);
}