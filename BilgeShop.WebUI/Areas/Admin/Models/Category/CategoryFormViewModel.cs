using System.ComponentModel.DataAnnotations;

namespace BilgeShop.WebUI.Areas.Admin.Models.Category
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Ad Alanı Zorunludur")]
        public string Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
    }
}
