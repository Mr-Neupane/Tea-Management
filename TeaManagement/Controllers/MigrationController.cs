using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Entities;

namespace TeaManagement.Controllers;

public class MigrationController : Controller
{
    private readonly AppDbContext _context;
    private readonly IToastNotification _toastNotification;

    public MigrationController(AppDbContext context, IToastNotification toastNotification)
    {
        _context = context;
        _toastNotification = toastNotification;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Apply()
    {
        try
        {
            var existingCoa = await _context.ChartOfAccounts.Where(x => x.Id == -1).FirstOrDefaultAsync();
            if (existingCoa == null)
            {
                var coaLedger = new List<CoaLedger>()
                {
                    new()
                    {
                        Id = -1,
                        Name = "Assets"
                    },
                    new()
                    {
                        Id = -2,
                        Name = "Liabilities"
                    },
                    new()
                    {
                        Id = -3,
                        Name = "Income"
                    },
                    new()
                    {
                        Id = -4,
                        Name = "Expenses"
                    }
                };
                await _context.ChartOfAccounts.AddRangeAsync(coaLedger);
                await _context.SaveChangesAsync();
            }


            var existingLedger = await _context.Ledgers.Where(x => x.Id == -1).FirstOrDefaultAsync();
            if (existingLedger==null)
            {
                var defLedger = new List<Ledger>()
                {
                    new()
                    {
                        Id = -1,
                        Name = "Cash Account",
                        Code = "80",
                        ParentId = -1,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = -2,
                        Name = "Bank Account",
                        Code = "90",
                        ParentId = -1,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = -8,
                        Name = "Stakeholder/Factory Account",
                        Code = "120",
                        ParentId = -1,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = -3,
                        Name = "Other Income",
                        Code = "160.1",
                        ParentId = -3,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = -4,
                        Name = "Other Expenses",
                        Code = "150.1",
                        ParentId = -4,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = -5,
                        Name = "Current Liabilities",
                        Code = "60",
                        ParentId = -2,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = -6,
                        Name = "Other Liabilities",
                        Code = "70",
                        ParentId = -2,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = -7,
                        Name = "Cash",
                        Code = "80.1",
                        ParentId = null,
                        SubParentId = -1
                    }
                };
                await _context.Ledgers.AddRangeAsync(defLedger);
                await _context.SaveChangesAsync();
            }

            _toastNotification.AddSuccessToastMessage("Migration added successfully.");
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(e.Message);
            return RedirectToAction("Index");
        }
    }
}