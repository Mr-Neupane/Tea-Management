using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("payable", Schema = "accounting")]
    public class Payable : BaseEntity
    {
        public int StakeholderId { get; set; }
        public decimal Amount { get; set; }
    }
}