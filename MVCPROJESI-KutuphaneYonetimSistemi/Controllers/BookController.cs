using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using MVCPROJESI_KutuphaneYonetimSistemi.Models;
using MVCPROJESI_KutuphaneYonetimSistemi.Services;
using MVCPROJESI_KutuphaneYonetimSistemi.ViewModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace MVCPROJESI_KutuphaneYonetimSistemi.Controllers
{
    //Login yapılmadan erişilmesini engeller
    [Authorize]
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAuthorService _authorService;

        public BookController(IAuthorService authorService, IWebHostEnvironment hostEnvironment)
        {
            _authorService = authorService;
            _hostEnvironment = hostEnvironment;
        }

        public static List<Book> _books = new List<Book>()
{
    new Book { Id = 1, Title = "Yüzüklerin Efendisi: İki Kule", AuthorId = 1, Genre = "Fantastik Kurgu", PublishDate = new DateTime(1954, 11, 11), ISBN = "1234567890", CopiesAvailable = 80, ImageUrl = "/images/İki_Kule.jpg" },
    new Book { Id = 2, Title = "Hobbit", AuthorId = 1, Genre = "Fantastik Kurgu", PublishDate = new DateTime(1937, 9, 21), ISBN = "1234567891", CopiesAvailable = 60, ImageUrl = "/images/Hobbit.jpg" },

    new Book { Id = 3, Title = "1984", AuthorId = 2, Genre = "Dystopik", PublishDate = new DateTime(1949, 6, 8), ISBN = "9780451524935", CopiesAvailable = 50, ImageUrl = "/images/1984.jpg" },
    new Book { Id = 4, Title = "Hayvan Çiftliği", AuthorId = 2, Genre = "Alegori", PublishDate = new DateTime(1945, 8, 17), ISBN = "9780451526342", CopiesAvailable = 75, ImageUrl = "/images/Hayvan_Ciftligi.jpg" },

    new Book { Id = 5, Title = "Ben, Robot", AuthorId = 3, Genre = "Bilim Kurgu", PublishDate = new DateTime(1950, 12, 2), ISBN = "1234567892", CopiesAvailable = 40, ImageUrl = "/images/Ben_Robot.jpg" },
    new Book { Id = 6, Title = "Vakıf", AuthorId = 3, Genre = "Bilim Kurgu", PublishDate = new DateTime(1951, 5, 1), ISBN = "1234567893", CopiesAvailable = 65, ImageUrl = "/images/Vakif.jpg" },

    new Book { Id = 7, Title = "Harry Potter ve Felsefe Taşı", AuthorId = 4, Genre = "Fantastik", PublishDate = new DateTime(1997, 6, 26), ISBN = "9780747532743", CopiesAvailable = 85, ImageUrl = "/images/Harry_Potter1.jpg" },
    new Book { Id = 8, Title = "Harry Potter ve Sırlar Odası", AuthorId = 4, Genre = "Fantastik", PublishDate = new DateTime(1998, 7, 2), ISBN = "9780747538486", CopiesAvailable = 90, ImageUrl = "/images/Harry_Potter2.jpg" },

    new Book { Id = 9, Title = "Suç ve Ceza", AuthorId = 5, Genre = "Klasik", PublishDate = new DateTime(1866, 1, 1), ISBN = "9786053604536", CopiesAvailable = 30, ImageUrl = "/images/Suc_ve_Ceza.jpg" },
    new Book { Id = 10, Title = "Karamazov Kardeşler", AuthorId = 5, Genre = "Klasik", PublishDate = new DateTime(1880, 11, 1), ISBN = "9786053604543", CopiesAvailable = 20, ImageUrl = "/images/Karamazov_Kardesler.jpg" }
};


        public IActionResult List()
        {
            var authors = _authorService.GetAuthors();
            var viewModel = _books.Where(x => !x.IsDeleted).Select(x => new BookViewModel
            {
                Id = x.Id,
                Title = x.Title,
                AuthorId = x.AuthorId,
                AuthorName = authors.FirstOrDefault(a => a.Id == x.AuthorId)?.FullName,
                Genre = x.Genre,
                PublishDate = x.PublishDate,
                ISBN = x.ISBN,
                CopiesAvailable = x.CopiesAvailable,
                ImageUrl = x.ImageUrl
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = _authorService.GetAuthors();
            var viewModel = new BookCreateViewModel
            {
                Authors = authors.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(BookCreateViewModel formdata)
        {
            if (!ModelState.IsValid)
            {
                var authors = _authorService.GetAuthors();
                formdata.Authors = authors.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                });

                // Hata mesajlarını günlüğe yazdır
                foreach (var state in ModelState)
                {
                    var errors = state.Value.Errors;
                    if (errors.Count > 0)
                    {
                        foreach (var error in errors)
                        {
                            Console.WriteLine($"Hata: {state.Key} - {error.ErrorMessage}");
                        }
                    }
                }

                return View(formdata);
            }

            // Resim dosyasını işleme
            string uniqueFileName = null;
            if (formdata.ImageFile != null && formdata.ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(formdata.ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formdata.ImageFile.CopyTo(fileStream);
                }
            }

            int maxId = _books.Max(x => x.Id);

            var newBook = new Book()
            {
                Id = maxId + 1,
                Title = formdata.Title,
                AuthorId = formdata.AuthorId,
                Genre = formdata.Genre,
                PublishDate = formdata.PublishDate,
                ISBN = formdata.ISBN,
                CopiesAvailable = formdata.CopiesAvailable,
                IsDeleted = false,
                ImageUrl = uniqueFileName != null ? $"/images/{uniqueFileName}" : null
            };

            _books.Add(newBook);

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id && !b.IsDeleted);
            if (book == null)
            {
                return NotFound();
            }

            var authors = _authorService.GetAuthors();
            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                AuthorName = authors.FirstOrDefault(a => a.Id == book.AuthorId)?.FullName,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                ImageUrl = book.ImageUrl
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.IsDeleted = true;
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id && !b.IsDeleted);
            if (book == null)
            {
                return NotFound();
            }

            var authors = _authorService.GetAuthors();
            var viewModel = new BookEditViewModel
            {
                Id = book.Id,
                Title = book.Title,
                AuthorId = book.AuthorId,
                Genre = book.Genre,
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                CopiesAvailable = book.CopiesAvailable,
                Authors = authors.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                }),
                ImageUrl = book.ImageUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(BookEditViewModel formdata)
        {
            if (!ModelState.IsValid)
            {
                var authors = _authorService.GetAuthors();
                formdata.Authors = authors.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                });
                return View(formdata);
            }

            var book = _books.FirstOrDefault(b => b.Id == formdata.Id && !b.IsDeleted);
            if (book != null)
            {
                book.Title = formdata.Title;
                book.AuthorId = formdata.AuthorId;
                book.Genre = formdata.Genre;
                book.PublishDate = formdata.PublishDate;
                book.ISBN = formdata.ISBN;
                book.CopiesAvailable = formdata.CopiesAvailable;
                if (formdata.ImageFile != null && formdata.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(formdata.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        formdata.ImageFile.CopyTo(fileStream);
                    }

                    book.ImageUrl = $"/images/{uniqueFileName}";
                }
            }

            return RedirectToAction("List");
        }
    }
}
