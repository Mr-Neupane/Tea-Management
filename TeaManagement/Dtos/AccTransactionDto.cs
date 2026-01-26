namespace TeaManagement.Dtos;

public class AccTransactionDto
{
    public DateTime TxnDate { get; set; }
    public string TxnType { get; set; }
    public int TypeId { get; set; }
    public decimal Amount { get; set; }

    public List<AccTransactionDetailsDto> Details { get; set; }
}

public class AccTransactionDetailsDto
{
    
    public int LedgerId { get; set; }
    public bool IsDr { get; set; }
    public decimal Amount { get; set; }
    public bool IsJv { get; set; } = false;
}