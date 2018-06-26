using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Olga.DAL.Entities.Account;

namespace Olga.Models
{
    public class RegisterModel
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Rank { get; set; }
        [Required]
        public string Name { get; set; }

        public Roles Role { get; set; }
    }
}