namespace TeaManagement.Dtos;

public class NewLedgerDto
{
    public string LedgerName { get; set; }
    public string? LedgerCode { get; set; }
    public int? SubParentId { get; set; }
    public int? ParentId { get; set; }
    public bool IsParent { get; set; } = false;
}