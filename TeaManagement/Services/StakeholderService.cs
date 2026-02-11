using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Enums;
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
        var code = GetStakeholderNumber(dto.IsSupplier);
        var ins = new Stakeholder
        {
            StakeholderType = dto.IsSupplier ? (int)StakeholderType.Supplier : (int)StakeholderType.Customer,
            FullName = dto.FullName,
            StakeholderCode = code,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            LedgerId = dto.LedgerId,
        };
        await _context.Stakeholders.AddAsync(ins);
        await _context.SaveChangesAsync();
        return ins;
    }

    private string GetStakeholderNumber(bool isSupplier)
    {
        if (isSupplier)
        {
            var cn = _context.Stakeholders.Count(x => x.StakeholderType == (int)StakeholderType.Supplier)+1;
            const string prefix = "S0000";
            var stakeholderNumber = string.Concat(prefix, cn);
            return stakeholderNumber;
        }
        else
        {
            var cn = _context.Stakeholders.Count(x => x.StakeholderType == (int)StakeholderType.Customer)+1;
            const string prefix = "C0000";
            var stakeholderNumber = string.Concat(prefix, cn);
            return stakeholderNumber;
        }
    }
}