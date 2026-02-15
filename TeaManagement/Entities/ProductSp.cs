using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("sales_price", Schema = "inventory")]
    public class ProductSp : BaseEntity
    {
        public int ProductId { get; set; }
        public int? ClassId { get; set; }
        public virtual Product Product { get; set; }
    }
}