using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVCPROJESI_KutuphaneYonetimSistemi.Models;
using MVCPROJESI_KutuphaneYonetimSistemi.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCPROJESI_KutuphaneYonetimSistemi.Controllers
{
    public class AuthController : Controller
    {
        private static List<User> _users = new List<User>();

        // SignUp: Yeni üye kaydı
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(SignUpViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                // ModelState geçerli değilse, formu tekrar göster
                return View(formData);
            }

            // Aynı e-posta ile kayıtlı başka bir kullanıcı var mı?
            if (_users.Any(u => u.Email == formData.Email))
            {
                ModelState.AddModelError("Email", "Bu e-posta adresi zaten kayıtlı.");
                return View(formData);
            }

            // Yeni kullanıcı oluştur ve listeye ekle
            var newUser = new User
            {
                Id = _users.Count + 1, // Benzersiz kimlik
                FullName = formData.FullName,
                Email = formData.Email,
                Password = formData.Password,
                PhoneNumber = formData.PhoneNumber,
                JoinDate = DateTime.Now // Otomatik olarak şu anki tarihi ayarlar
            };

            _users.Add(newUser);
            return RedirectToAction("Login");
        }



        // Login: Üye girişi
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }

            // Girilen e-posta ve parolaya sahip bir kullanıcı var mı?
            var user = _users.FirstOrDefault(u => u.Email == formData.Email && u.Password == formData.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya parola.");
                return View(formData);
            }

            // Başarılı giriş
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true // Çerezin kalıcı olmasını sağlar
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("List", "Book");
        }


        public IActionResult Welcome()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.FullName = User.Identity.Name; // Kullanıcı adını alıyoruz
                return View();
            }
            return RedirectToAction("Login");
        }

       [HttpPost]
public async Task<IActionResult> Logout()
{
    // Oturumu kapat
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    
    // Ana sayfaya yönlendir
    return RedirectToAction("Index", "Home");
}
    }
}
