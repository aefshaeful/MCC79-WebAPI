using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class NewAccountDto
    {
        [Required]
        public Guid Guid { get; set; }
        [PasswordPolicy]
        public string Password { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int Otp { get; set; }
        [Required]
        public bool IsUsed { get; set; }
        /*public DateTime ExpiredTime { get; set; }*/
    }
}
