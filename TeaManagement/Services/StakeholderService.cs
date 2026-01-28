using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class StakeholderService : IStakeholderService
{
    private readonly ApplicationDbContext _context;

    public StakeholderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Stakeholder> RecordStakeholderAsync(StakeholderDto dto)
    {
        var ins = new Stakeholder
        {
            StakeholderType = dto.StakeholderType,
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            LedgerId = dto.LedgerId,
        };
        await _context.Stakeholders.AddAsync(ins);
        await _context.SaveChangesAsync();
        return ins;
    }
}