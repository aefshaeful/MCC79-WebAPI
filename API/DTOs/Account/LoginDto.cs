﻿using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPolicy]
        public string Password { get; set; }
    }
}
