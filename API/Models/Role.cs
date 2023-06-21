using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    [Table("tb_tr_roles")]
    public class Role : BaseEntity
    {
        [Column("name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
    }
}
