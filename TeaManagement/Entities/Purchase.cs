using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("purchase", Schema = "inventory")]
    public class Purchase : BaseEntity
    {
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime TxnDate { get; set; }

        public List<PurchaseDetails> PurchaseDetails { get; set; }
    }
}