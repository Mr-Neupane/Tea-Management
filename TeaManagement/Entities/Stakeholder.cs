using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("stakeholders", Schema = "stakeholder")]
    public class Stakeholder : BaseEntity
    {
        public int StakeholderType { get; set; }
        public string FullName { get; set; }
        
        public string StakeholderCode { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }
    }
}