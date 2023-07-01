using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class GetRegisterDto
    {
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
        [Required]
        public string Major { get; set; }
        [Required]
        public string Degree { get; set; }
        [Range(0, 4, ErrorMessage = "GPA is only from 0 to 4")]
        public double Gpa { get; set; }
        [Required]
        public string UniversityCode { get; set; }
        [Required]
        public string UniversityName { get; set; }
        [PasswordPolicy]
        public string Password { get; set; }
        [ConfirmPassword("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
