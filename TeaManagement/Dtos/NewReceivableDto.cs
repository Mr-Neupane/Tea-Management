namespace TeaManagement.Dtos;

public class NewReceivableDto
{
    public int StakeholderId { get; set; }
    public DateTime TxnDate { get; set; }
    public decimal Amount { get; set; }
    public int TransactionId { get; set; }
}