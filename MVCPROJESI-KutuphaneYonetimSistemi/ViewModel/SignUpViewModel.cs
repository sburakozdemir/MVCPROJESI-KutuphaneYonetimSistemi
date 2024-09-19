using System.ComponentModel.DataAnnotations;

namespace MVCPROJESI_KutuphaneYonetimSistemi.ViewModel
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Ad ve soyad gereklidir.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarını girin.")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        public string PhoneNumber { get; set; }
    }
}
