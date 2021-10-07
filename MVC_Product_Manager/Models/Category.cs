using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Product_Manager.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Kategorie")]
        public string CategoryName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }
        [StringLength(160)]
        [Display(Name = "Kategoriebild")]
        public string Image { get; set; }
        [NotMapped]
        [Display(Name ="Bildupload")]
        public IFormFile ImageFile { get; set; }
    }
}
