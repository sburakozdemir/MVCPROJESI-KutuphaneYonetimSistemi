using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCPROJESI_KutuphaneYonetimSistemi.Models;
using MVCPROJESI_KutuphaneYonetimSistemi.Services;
using MVCPROJESI_KutuphaneYonetimSistemi.ViewModel;
using System.Linq;

namespace MVCPROJESI_KutuphaneYonetimSistemi.Controllers
{
    //Login yapılmadan erişilmesini engeller
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // 1. Yazarların Listesi (List)
        public IActionResult List()
        {
            var authors = _authorService.GetAuthors();
            var viewModel = authors.Select(a => new AuthorViewModel
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                DateOfBirth = a.DateOfBirth 
            }).ToList();

            return View(viewModel);
        }

        // 2. Yazarın Detayları (Details)
        public IActionResult Details(int id)
        {
            var author = _authorService.GetAuthors().FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            var books = BookController._books
                .Where(b => b.AuthorId == id && !b.IsDeleted)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Genre = b.Genre,
                    PublishDate = b.PublishDate,
                    ISBN = b.ISBN,
                    CopiesAvailable = b.CopiesAvailable,
                    ImageUrl = b.ImageUrl,
                })
                .ToList();

            var viewModel = new AuthorDetailsViewModel
            {
                Id = author.Id,
                FullName = $"{author.FirstName} {author.LastName}",
                DateOfBirth = author.DateOfBirth,
                Books = books 
            };

            return View(viewModel);
        }

        // 3. Yeni Yazar Ekleme (Create) - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. Yeni Yazar Ekleme (Create) - POST
        [HttpPost]
        public IActionResult Create(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newAuthor = new Author
                {
                    Id = _authorService.GetAuthors().Max(a => a.Id) + 1,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth, 
                    IsDeleted = false
                };

                // Author'ı listeye ekle
                _authorService.AddAuthor(newAuthor);
                return RedirectToAction("List");
            }

            return View(model);
        }

        // 4. Yazar Düzenleme (Edit) - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = _authorService.GetAuthors().FirstOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            var viewModel = new AuthorViewModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth 
            };

            return View(viewModel);
        }

        // 4. Yazar Düzenleme (Edit) - POST
        [HttpPost]
        public IActionResult Edit(int id, AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = _authorService.GetAuthors().FirstOrDefault(a => a.Id == id);

                if (author == null)
                    return NotFound();

                // Yazar bilgilerini güncelle
                author.FirstName = model.FirstName;
                author.LastName = model.LastName;
                author.DateOfBirth = model.DateOfBirth; 

                return RedirectToAction("List");
            }

            return View(model);
        }

        // 5. Yazar Silme (Delete) - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var author = _authorService.GetAuthors().FirstOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            var viewModel = new AuthorViewModel
            {
                Id = author.Id,
             
            };

            return View(viewModel);
        }

        // 5. Yazar Silme (Delete) - POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var author = _authorService.GetAuthors().FirstOrDefault(a => a.Id == id);

            if (author == null)
                return NotFound();

            // Yazar silme işlemi (IsDeleted = true yapılıyor)
            author.IsDeleted = true;

            return RedirectToAction("List");
        }
    }
}
