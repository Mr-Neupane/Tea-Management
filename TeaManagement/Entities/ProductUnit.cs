using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("unit",Schema = "inventory")]
    
    public class ProductUnit: BaseEntity
    {
    public string UnitName { get; set; }
    public string? UnitDescription { get; set; }
    }
}
