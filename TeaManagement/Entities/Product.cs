using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("product", Schema = "general_setup")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}