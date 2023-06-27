using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        [Required]
        public Guid Guid { get; set; }
        [Required] 
        public string Nik { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "0 to Female, 1 to Male")]
        public GenderEnum Gender { get; set; }
        [Required]
        public DateTime HiringDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
