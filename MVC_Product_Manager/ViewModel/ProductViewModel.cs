using System.ComponentModel.DataAnnotations;

namespace MVC_Product_Manager.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Product Beschreibung")]
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
    }
}
