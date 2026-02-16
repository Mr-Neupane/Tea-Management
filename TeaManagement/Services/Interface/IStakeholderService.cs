using TeaManagement.Dtos;
using TeaManagement.Entities;

namespace TeaManagement.Interface;

public interface IStakeholderService
{
    public Task<Stakeholder> RecordStakeholderAsync(StakeholderDto dto);
    public Task DeactivateStakeholderAsync(int stakeholderId);
}
