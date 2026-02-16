using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using TeaManagement.Constraints;
using TeaManagement.Entities;

namespace TeaManagement.Controllers;

public class MigrationController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IToastNotification _toastNotification;

    public MigrationController(ApplicationDbContext context, IToastNotification toastNotification)
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
            if (existingLedger == null)
            {
                var defLedger = new List<Ledger>()
                {
                    new()
                    {
                        Id = ParentLedgerIdConstraints.CashAccount,
                        Name = "Cash Account",
                        Code = "80",
                        ParentId = -1,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.BankAccount,
                        Name = "Bank Account",
                        Code = "90",
                        ParentId = -1,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.Debtors,
                        Name = "Stakeholder/Factory Account",
                        Code = "120",
                        ParentId = -1,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.OtherIncome,
                        Name = "Other Income",
                        Code = "160.2",
                        ParentId = -3,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.SamanBikriAccount,
                        Name = "Saman Bikri Account",
                        Code = "160.1",
                        ParentId = -3,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = LedgerIdConstraints.Sales,
                        Name = "Stock Sales",
                        Code = "160.1.1",
                        ParentId = null,
                        SubParentId = -12
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.OtherExpenses,
                        Name = "Other Expenses",
                        Code = "150.2",
                        ParentId = -4,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.SamanKharidAccount,
                        Name = "Saman Kharid",
                        Code = "150.1",
                        ParentId = -4,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = LedgerIdConstraints.Purchase,
                        Name = "Stock Purchase",
                        Code = "150.1.1",
                        ParentId = null,
                        SubParentId = -10
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.CurrentLiabilities,
                        Name = "Current Liabilities",
                        Code = "60",
                        ParentId = -2,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.OtherLiabilities,
                        Name = "Other Liabilities",
                        Code = "70",
                        ParentId = -2,
                        SubParentId = null
                    },
                    new()
                    {
                        Id = LedgerIdConstraints.Cash,
                        Name = "Cash",
                        Code = "80.1",
                        ParentId = null,
                        SubParentId = -1
                    },
                    new()
                    {
                        Id = ParentLedgerIdConstraints.Creditors,
                        Name = "Creditors",
                        Code = "50",
                        ParentId = -2,
                        SubParentId = null
                    }
                };
                await _context.Ledgers.AddRangeAsync(defLedger);
                await _context.SaveChangesAsync();
            }

            var existingCat = await _context.Categories.Where(x => x.Id == -1).FirstOrDefaultAsync();
            if (existingCat == null)
            {
                var defCat = new Category
                {
                    Id = -1,
                    Name = "Tea",
                    ParentCategoryId = null
                };
                await _context.Categories.AddAsync(defCat);
                await _context.SaveChangesAsync();
            }

            var existingUnit = await _context.ProductUnits.Where(x => x.Id == -1).FirstOrDefaultAsync();
            if (existingUnit == null)
            {
                var unit = new ProductUnit
                {
                    Id = -1,
                    UnitName = "KG",
                    UnitDescription = "Kilogram"
                };
                await _context.ProductUnits.AddAsync(unit);
                await _context.SaveChangesAsync();
            }

            var existingProd = await _context.Products.Where(x => x.Id == -1).FirstOrDefaultAsync();
            if (existingProd == null)
            {
                var defProd = new Product
                {
                    Id = -1,
                    Name = "Green Tea Leaves",
                    CategoryId = -1,
                    UnitId = -1
                };
                await _context.Products.AddAsync(defProd);
                await _context.SaveChangesAsync();
            }


            var existingClass = await _context.TeaClass.Where(x => x.Id == -1).FirstOrDefaultAsync();
            if (existingClass == null)
            {
                var teaClass = new List<TeaClass>
                {
                    new()
                    {
                        Id = -1,
                        Name = "SF",
                        Description = "Super Fine"
                    },
                    new()
                    {
                        Id = -2,
                        Name = "A",
                        Description = "Excellent Quality"
                    },
                    new()
                    {
                        Id = -3,
                        Name = "B",
                        Description = "Good Quality"
                    },
                    new()
                    {
                        Id = -4,
                        Name = "C",
                        Description = "Average Grade"
                    },
                    new()
                    {
                        Id = -5,
                        Name = "C-",
                        Description = "Not So Good/ Worst Grade"
                    }
                };
                await _context.TeaClass.AddRangeAsync(teaClass);
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