namespace TeaManagement.Dtos;

public class AddBonusDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public DateTime EffDate { get; set; } = DateTime.Now;
    public int FactoryId { get; set; }
    public int? LedgerId { get; set; }
    public decimal BonusPerKg { get; set; }
    public bool IsNewLedger { get; set; } = false;
}