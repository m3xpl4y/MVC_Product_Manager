using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_Product_Manager.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Product_Manager.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Kategorie")]
        public string CatName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }
        [StringLength(160)]
        [Display(Name = "Kategorie Bild")]
        public string Image { get; set; }
        List<Product> Products { get; set; } = new List<Product>();
    }
}
