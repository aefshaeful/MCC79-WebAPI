using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class ChangePasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int Otp { get; set; }
        [PasswordPolicy]
        public string NewPassword { get; set; }
        [ConfirmPassword("NewPassword")]
        public string ConfirmPassword { get; set; }


        /*<remarks>
        Silahkan buat endpoint untuk ChangePassword digunakan untuk set password baru dengan property yang diinput adalah:
        Email, OTP, NewPassword, ConfirmPassword
        dengan handler seperti berikut:
        *jika otp salah, error
        * jika otp sudah digunakan, error
        * jika otp sudah expired, error
        * jika confirmpassword != newpassword, error*/
    }
}
