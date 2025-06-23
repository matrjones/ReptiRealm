﻿using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Roles are required")]
        public string[] Roles { get; set; }
    }
}
