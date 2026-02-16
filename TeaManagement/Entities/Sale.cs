using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("sales", Schema = "inventory")]
    public class Sale : BaseEntity
    {
        public int FactoryId { get; set; }
        public DateTime TxnDate { get; set; }
        public string SaleNo { get; set; }
        public decimal Amount { get; set; }
        public string? BillNo { get; set; }

        public List<SaleDetails> SalesDetails { get; set; }

        public virtual NewFactory Factory { get; set; }
    }
}