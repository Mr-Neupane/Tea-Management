using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("purchase_details", Schema = "inventory")]
    public class PurchaseDetails : BaseEntity
    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Discount { get; set; }
        public virtual ProductUnit Unit { get; set; }
        public virtual Purchase Purchase { get; set; }
        public virtual Product Product { get; set; }
    }
}