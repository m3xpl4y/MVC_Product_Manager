using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Product_Manager.Models
{
    public class Category
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
    }
}
