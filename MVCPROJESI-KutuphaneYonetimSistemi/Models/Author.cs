namespace MVCPROJESI_KutuphaneYonetimSistemi.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FullName => FirstName + " " + LastName;
        public bool IsDeleted { get; set; }
    }
}
