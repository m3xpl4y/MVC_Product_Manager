using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Product_Manager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(60)]
        [Display(Name = "Firma")]
        public string Company { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Vorname")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Nachname")]
        public string LastName { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Geburtstag")]
        public DateTime Birthday { get; set; }
        [StringLength(60)]
        [Display(Name = "Straße")]
        public string Street { get; set; }
        [StringLength(35)]
        [Display(Name = "Stadt")]
        public string City { get; set; }
    }
}
