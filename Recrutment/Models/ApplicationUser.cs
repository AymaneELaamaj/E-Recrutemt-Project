﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace Recrutment.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]  
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }

    }
}