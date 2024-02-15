using System.ComponentModel.DataAnnotations;

namespace BilgeShop.WebUI.Areas.Admin.Models.Product
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Display(Name="İsim")]
        [Required(ErrorMessage ="Ürün ismi girmek zorunludur.")]
        public string Name { get; set; }

        [Display(Name="Açıklama")]
        public string? Description { get; set; }
        
        [Display(Name="Fiyat")]
        public decimal? UnitPrice { get; set; }

        [Display(Name="Stok Miktarı")]
        public int UnitsInStock { get; set; }

        [Display(Name="Kategori")]
        [Required(ErrorMessage ="Kategori seçmek zorunludur")]
        public int CategoryId { get; set; }
        
        [Display(Name="Ürün Görseli")]
        public IFormFile? File { get; set; }


    }
}
