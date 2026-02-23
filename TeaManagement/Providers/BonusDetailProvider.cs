using Microsoft.EntityFrameworkCore;
using TeaManagement.Dtos;
using TeaManagement.Enums;

namespace TeaManagement.Providers;

public class BonusDetailProvider
{
    private readonly ApplicationDbContext _context;

    public BonusDetailProvider(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BonusDetailDto?> GetBonusDetail(int factoryId, DateTime effDate)
    {
        var res = await _context.Bonus.Where(x =>
                x.FactoryId == factoryId && x.Status == (int)Status.Active && x.EffectiveDate <= effDate)
            .OrderByDescending(x => x.EffectiveDate).Select(x => new BonusDetailDto
            {
                LedgerId = x.LedgerId,
                BonusPerKg = x.BonusPerKg
            })
            .FirstOrDefaultAsync();
        return res;
    }
}