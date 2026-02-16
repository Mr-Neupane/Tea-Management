using System.Threading.Tasks;
using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface IFactoryService
{
    public Task<NewFactory> AddFactoryAsync(NewFactoryDto dto);
    public Task<NewFactory> GetFactoryByIdAsync(int id);

    public Task DeactivateFactoryAsync(int factoryId);
}