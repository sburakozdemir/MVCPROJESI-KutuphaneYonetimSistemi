using System.ComponentModel.DataAnnotations;

namespace MVCPROJESI_KutuphaneYonetimSistemi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; } // Yazarın ID'si
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public int CopiesAvailable { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } // Silinmiş kitapları işaretlemek için
    }

}
