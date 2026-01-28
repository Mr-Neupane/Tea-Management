using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("receivable", Schema = "accounting")]
    public class Receivable : BaseEntity
    {
        public int StakeholderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TxnDate { get; set; }
        public int TransactionId { get; set; }
        public virtual AccountingTransaction Transaction { get; set; }
        public virtual Stakeholder Stakeholder { get; set; }
    }
}