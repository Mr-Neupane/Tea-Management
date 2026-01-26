using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeaManagement.ViewModels;

public class AddLegderVm
{
    public string LedgerName { get; set; }
    public string? LedgerCode { get; set; }
    public int? SubParentId { get; set; }
    public int? ParentId { get; set; }
    public SelectList SubParentIds { get; set; }
}