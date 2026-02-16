namespace TeaManagement.Dtos;

public class PayableDto
{
    public int StakeholderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PayableDate { get; set; }
}