using System.ComponentModel.DataAnnotations.Schema;

namespace TeaManagement.Entities
{
    [Table("coa", Schema = "accounting")]
    public class CoaLedger : BaseEntity
    {
       public string Name { get; set; }

    }
}

