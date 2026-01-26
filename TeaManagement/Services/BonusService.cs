using TeaManagement.Dtos;
using TeaManagement.Entities;
using TeaManagement.Enums;
using TeaManagement.Interface;

namespace TeaManagement.Services;

public class BonusService : IBonusService
{
    private readonly ApplicationDbContext _context;

    public BonusService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AddBonus> AddBonusAsync(AddBonusDto dto)
    {
        var bonus = new AddBonus
        {
            RecStatus = (char)RecStatus.Active,
            Status = (int)Status.Active,
            RecById = -1,
            Name = dto.Name,
            FactoryId = dto.FactoryId,
            BonusPerKg = dto.BonusPerKg,
            LedgerId = dto.LedgerId,
        };
        await _context.Bonus.AddAsync(bonus);
        await _context.SaveChangesAsync();
        return bonus;
    }
}