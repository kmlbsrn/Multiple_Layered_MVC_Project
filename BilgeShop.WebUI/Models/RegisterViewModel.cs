using System.ComponentModel.DataAnnotations;

namespace BilgeShop.WebUI.Models
{
    public class RegisterViewModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Email kısmı boş bırakılamaz")]
        public string Email { get; set; }

        [Display(Name ="İsim")]
        [MaxLength(25,ErrorMessage ="Ad Alanı en fazla 25 karakter uzunluğunda olmalı")]
        [Required(ErrorMessage ="Ad Alanı Boş Bırakılamaz")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(25, ErrorMessage = "Soyad Alanı en fazla 25 karakter uzunluğunda olmalı")]
        [Required(ErrorMessage = "Soyad Alanı Boş Bırakılamaz")]
        public string LastName { get; set; }

        [Display(Name ="Şifre")]
        [Required(ErrorMessage ="Şifre Alanı boş bırakılamaz")]
        public string Password { get; set; }


        [Display(Name = "Şifre Tekrarı")]
        [Required(ErrorMessage = " Tekrar Şifre Alanı boş bırakılamaz")]
        [Compare(nameof(Password),ErrorMessage ="Şifreler eşleşmiyor")]
        public string PasswordConfirm { get; set; }

    }
}
