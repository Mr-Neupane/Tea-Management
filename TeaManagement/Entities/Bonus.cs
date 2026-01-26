using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("bonus", Schema = "general_setup")]
    public class AddBonus : BaseEntity
    {
        public string Name { get; set; }
        public int FactoryId { get; set; }
        public decimal BonusPerKg { get; set; }
        public int? LedgerId { get; set; }
        public virtual NewFactory Factory { get; set; }
    }
}