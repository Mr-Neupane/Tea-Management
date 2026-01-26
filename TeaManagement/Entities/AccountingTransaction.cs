using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("transactions", Schema = "accounting")]
    public class AccountingTransaction : BaseEntity
    {
        public DateTime TransactionDate { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public bool IsReversed { get; set; } = false;
        public int? ReversedId { get; set; }
    }
}