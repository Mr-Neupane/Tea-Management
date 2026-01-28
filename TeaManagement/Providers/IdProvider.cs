using Microsoft.EntityFrameworkCore;

namespace TeaManagement.Providers;

public class IdProvider
{
    private readonly ApplicationDbContext _context;

    public IdProvider(ApplicationDbContext context)
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

    public int GetStakeholderIdByLedgerId(int ledgerId)
    {
        var res = _context.Stakeholders.SingleOrDefault(x => x.LedgerId == ledgerId);
        if (res != null)
        {
            return res.LedgerId;
        }
        else
        {
            throw new Exception("Stakeholder not found");
        }
    }
}