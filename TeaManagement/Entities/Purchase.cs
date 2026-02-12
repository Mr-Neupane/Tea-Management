using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("purchase", Schema = "inventory")]
    public class Purchase : BaseEntity
    {
        public int SupplierId { get; set; }
        public string PurchaseNo { get; set; }
        public string? BillNo { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public DateTime TxnDate { get; set; }

        public virtual Stakeholder Supplier { get; set; }
        public List<PurchaseDetails> PurchaseDetails { get; set; }
    }
}