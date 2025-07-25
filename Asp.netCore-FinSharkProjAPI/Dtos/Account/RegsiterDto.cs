﻿using System.ComponentModel.DataAnnotations;

namespace Asp.netCore_FinSharkProjAPI.Dtos.Account
{
    public class RegsiterDto
    {
        [Required]
        public string? UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]

        public string Password { get; set; } = string.Empty;
        
    }
}
