using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVCPROJESI_KutuphaneYonetimSistemi.ViewModel
{
    public class BookCreateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap başlığı gereklidir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yazar seçilmelidir.")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Kitap türü gereklidir.")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Yayın tarihi gereklidir.")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "ISBN numarası gereklidir.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Kopya sayısı gereklidir.")]
        public int CopiesAvailable { get; set; }

        public IFormFile ImageFile { get; set; }
        public string? ImageUrl { get; set; } 

        public IEnumerable<SelectListItem>? Authors { get; set; } 
    }
}
