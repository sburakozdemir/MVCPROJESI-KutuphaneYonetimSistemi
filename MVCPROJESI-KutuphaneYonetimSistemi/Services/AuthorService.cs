using MVCPROJESI_KutuphaneYonetimSistemi.Models;
using MVCPROJESI_KutuphaneYonetimSistemi.Services;

public class AuthorService : IAuthorService
{
    private static List<Author> _authors = new List<Author>()
    {
        new Author { Id = 1, FirstName = "J.R.R.", LastName = "Tolkien", DateOfBirth = new DateTime(1892, 1, 3), IsDeleted = false },
        new Author { Id = 2, FirstName = "George", LastName = "Orwell", DateOfBirth = new DateTime(1903, 6, 25), IsDeleted = false },
        new Author { Id = 3, FirstName = "Isaac", LastName = "Asimov", DateOfBirth = new DateTime(1920, 1, 2), IsDeleted = false },
        new Author { Id = 4, FirstName = "J.K.", LastName = "Rowling", DateOfBirth = new DateTime(1965, 7, 31), IsDeleted = false },
        new Author { Id = 5, FirstName = "Fyodor", LastName = "Dostoevsky", DateOfBirth = new DateTime(1821, 11, 11), IsDeleted = false }
    };

    public List<Author> GetAuthors()
    {
        return _authors.Where(a => !a.IsDeleted).ToList();
    }


    public void AddAuthor(Author author)
    {
        _authors.Add(author);
    }
}
