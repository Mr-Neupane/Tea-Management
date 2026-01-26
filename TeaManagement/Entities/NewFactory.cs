using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("factory", Schema = "general_setup")]
    public class NewFactory : BaseEntity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public string Country { get; set; }
        public int LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }
    }
}