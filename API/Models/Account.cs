using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API.Models
{
    public class Account
    {
        [Key]
        [Column("guid")]
        public Guid Guid { get; set; }

        [Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }

        [Column("is_deleted")]
        public Boolean IsDeleted { get; set; }

        [Column("otp")]
        public int Otp { get; set; }

        [Column("is_used")]
        public Boolean IsUsed { get; set; }

        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
