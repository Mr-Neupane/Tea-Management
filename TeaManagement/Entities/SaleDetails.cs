using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("sale_details", Schema = "inventory")]
    public class SaleDetails : BaseEntity
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal WaterQuantity { get; set; } = 0;
        public decimal NetQuantity { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public virtual Product Product { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual ProductUnit Unit { get; set; }    }
}