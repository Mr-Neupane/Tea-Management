using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("ledger", Schema = "accounting")]
    public class Ledger : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ParentId { get; set; }
        public int? SubParentId { get; set; }
    }
}