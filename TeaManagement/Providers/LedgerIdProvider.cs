using Microsoft.EntityFrameworkCore;

namespace TeaManagement.Providers;

public class LedgerIdProvider
{
    private readonly ApplicationDbContext _context;

    public LedgerIdProvider(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetFactoryLedgerIdAsync(int factoryId)
    {
        var facLedgerId = await _context.Factories.Where(x => x.Id == factoryId).Select(x => x.LedgerId)
            .SingleOrDefaultAsync();
        if (facLedgerId == null)
        {
            throw new Exception("Factory not found");
        }
        else
        {
            return facLedgerId;
        }
    }
}