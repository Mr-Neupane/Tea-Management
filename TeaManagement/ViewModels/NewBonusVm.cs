using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeaManagement.ViewModels;

public class NewBonusVm
{
    public string Name { get; set; }
    public int FactoryId { get; set; }
    public int LedgerId { get; set; }
    public decimal BonusPerKg { get; set; }


    public bool IsNewLedger { get; set; }
    public SelectList BonusLedgerList { get; set; }
    public SelectList FactorySelectList { get; set; }
}