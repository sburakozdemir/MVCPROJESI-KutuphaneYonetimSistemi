using MVCPROJESI_KutuphaneYonetimSistemi.Models;

namespace MVCPROJESI_KutuphaneYonetimSistemi.Services
{
    public interface IAuthorService
    {
        List<Author> GetAuthors();
        void AddAuthor(Author author);
    }

}
