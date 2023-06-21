using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Employee
    {
        [Key]
        [Column("guid")]
        public Guid Guid { get; set; }

        [Column("nik", TypeName = "nchar(6)")]
        public string Nik { get; set; }

        [Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("gender")]
        public int Gender { get; set; }

        [Column("hiring_date")]
        public DateTime Hiringdate { get; set; }

        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
