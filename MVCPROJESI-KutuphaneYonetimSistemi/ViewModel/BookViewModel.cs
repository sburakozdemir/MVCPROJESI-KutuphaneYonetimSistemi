
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System;
namespace MVCPROJESI_KutuphaneYonetimSistemi.ViewModel

{

    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
        public DateTime PublishDate { get; set; }
        public string ISBN { get; set; }
        public int CopiesAvailable { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
    }


}