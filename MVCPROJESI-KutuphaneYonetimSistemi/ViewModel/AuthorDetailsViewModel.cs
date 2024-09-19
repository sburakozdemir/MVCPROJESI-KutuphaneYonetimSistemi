using System;
using System.Collections.Generic;

namespace MVCPROJESI_KutuphaneYonetimSistemi.ViewModel
{
    public class AuthorDetailsViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }

        // Yazarın kitapları burada listelenecek
        public List<BookViewModel> Books { get; set; }
    }
}
