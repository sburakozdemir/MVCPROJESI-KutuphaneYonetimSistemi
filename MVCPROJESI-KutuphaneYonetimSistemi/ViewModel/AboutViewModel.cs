using System.ComponentModel.DataAnnotations;

namespace MVCPROJESI_KutuphaneYonetimSistemi.ViewModel
{
    public class AboutViewModel
    {
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "İletişim Bilgileri")]
        public string ContactInfo { get; set; }
    }
}
