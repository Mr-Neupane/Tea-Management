using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("tea_class", Schema = "general_setup")]
    public class TeaClass : BaseEntity
    {
        public string Name{ get; set; }
        public string? Description{ get; set; }
        
    }
}