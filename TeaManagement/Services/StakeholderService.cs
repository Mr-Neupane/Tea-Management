using Microsoft.EntityFrameworkCore;
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
            var cn = _context.Stakeholders.Count(x => x.StakeholderType == (int)StakeholderType.Supplier) + 1;
            const string prefix = "S0000";
            var stakeholderNumber = string.Concat(prefix, cn);
            return stakeholderNumber;
        }
        else
        {
            var cn = _context.Stakeholders.Count(x => x.StakeholderType == (int)StakeholderType.Customer) + 1;
            const string prefix = "C0000";
            var stakeholderNumber = string.Concat(prefix, cn);
            return stakeholderNumber;
        }
    }

    public async Task DeactivateStakeholderAsync(int stakeholderId)
    {
        var stakeholder = await _context.Stakeholders.FindAsync(stakeholderId);

        if (stakeholder == null)
        {
            throw new Exception("Stakeholder not found");
        }
        else
        {
            if (stakeholder.StakeholderType == (int)StakeholderType.Supplier)
            {
                var payable = _context.Payables.Where(x =>
                    x.StakeholderId == stakeholder.Id && stakeholder.Status == (int)Status.Active).Sum(x => x.Amount);
                var paid = _context.Payables.Where(x =>
                    x.StakeholderId == stakeholder.Id && stakeholder.Status == (int)Status.Active).Sum(x => x.Amount);

                if (payable - paid == 0)
                {
                    stakeholder.Status = (int)Status.Inactive;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Payable amount must be zero to deactivate supplier.");
                }
            }
            else
            {
                var receivable = _context.Receivable.Where(x =>
                    x.StakeholderId == stakeholder.Id && stakeholder.Status == (int)Status.Active).Sum(x => x.Amount);
                var received = _context.Receivable.Where(x =>
                    x.StakeholderId == stakeholder.Id && stakeholder.Status == (int)Status.Active).Sum(x => x.Amount);

                if (receivable - received == 0)
                {
                    stakeholder.Status = (int)Status.Inactive;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Receivable amount must be zero to deactivate customer.");
                }
            }
        }
    }
}