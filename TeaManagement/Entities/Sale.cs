using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("sales", Schema = "inventory")]
    public class Sale : BaseEntity
    {
        public int ProductId { get; set; }
        public int FactoryId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal WaterQuantity { get; set; } = 0;
        public decimal NetQuantity { get; set; }
        public int TransactionId { get; set; }
        public virtual AccountingTransaction Transaction { get; set; }


        public virtual NewFactory Factory { get; set; }
        public virtual Product Product { get; set; }
    }
}