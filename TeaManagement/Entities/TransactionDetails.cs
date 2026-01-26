using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("transaction_details", Schema = "accounting")]
    public class TransactionDetails : BaseEntity
    {
        public int TransactionId { get; set; }
        public int LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }
        public char DrCr { get; set; }
        public decimal DrAmount { get; set; }
        public decimal CrAmount { get; set; }
        public virtual AccountingTransaction Transaction { get; set; }
    }
}