﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using API.Utilities.Enums;

namespace API.Models
{
    [Table("tb_m_employees")]
    public class Employee : BaseEntity
    {
        [Column("nik", TypeName = "nchar(6)")]
        public string Nik { get; set; }

        [Column("first_name", TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("gender")]
        public GenderEnum Gender { get; set; }

        [Column("hiring_date")]
        public DateTime Hiringdate { get; set; }

        [Column("email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column("phone_number", TypeName = "nvarchar(20)")]
        public string PhoneNumber { get; set; }


        // Cardinality
        public Education? Educations { get; set; }
        public ICollection<Booking>? Bookings { get; set;}
        public Account? Accounts { get; set; }
    }
}
