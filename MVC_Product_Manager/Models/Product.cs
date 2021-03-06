using System.ComponentModel.DataAnnotations;

namespace MVC_Product_Manager.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Produktname")]
        public string ProductName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Produktbeschreibung")]
        public string Description { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Artikelnummer")]
        public string ArtNr { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Hersteller")]
        public string Brand { get; set; }
        [StringLength(160)]
        [Display(Name = "Bild")]
        public string Image { get; set; }
        public Category Category { get; set; }
    }
}
