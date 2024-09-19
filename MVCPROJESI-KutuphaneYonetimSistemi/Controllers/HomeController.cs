using Microsoft.AspNetCore.Mvc;
using MVCPROJESI_KutuphaneYonetimSistemi.Models;
using MVCPROJESI_KutuphaneYonetimSistemi.ViewModel;
using System.Diagnostics;

namespace MVCPROJESI_KutuphaneYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpGet]
        public IActionResult About()
        {
            var model = new AboutViewModel
            {
                Title = "Rivendell (Imladris) Kütüphanesi: Bilgeliðin ve Bilimin Baþkenti",
                Description = "Orta Dünya'nýn kalbinde, esrarengiz daðlarýn arasýnda saklý bir vaha olarak bilinen Rivendell, ya da elflerin diliyle 'Imladris', sadece bir sýðýnak deðil, ayný zamanda yüzyýllardýr bilgelik ve öðrenmenin de merkezi olmuþtur. Imladris Kütüphanesi, bu köklü mirasý yaþatmak ve Orta Dünya'nýn dört bir yanýndan gelen bilgi arayýþýndaki ruhlara ýþýk tutmak amacýyla kurulmuþtur.\n\nBu kütüphanede, elflerin efsanevi þiirlerinden, insanlar ve cücelerin tarihi destanlarýna; hobbitlerin mütevazý yaþam öykülerinden, doðunun esrarengiz halklarýnýn kadim bilgilerine kadar sayýsýz eser yer alýr. Her bir kitap, büyülü sayfalarýnda sonsuz bir bilgi deryasý sunar. Kütüphanemiz, sadece kitaplardan ibaret deðil; burada el yazmalarý, nadir haritalar ve Orta Dünya’nýn kayýp halklarýna dair unutulmuþ belgeler de saklanmaktadýr.\n\nKütüphane, Lord Elrond'un rehberliðinde, hem geçmiþi araþtýrmak isteyen bilginlere hem de geleceði inþa etmek isteyen cesur ruhlara ev sahipliði yapar. Buraya adým atan her ziyaretçi, kadim bilgeliðin izini sürebilecek, öðrenme yolculuðunda eþsiz bir deneyim yaþayacaktýr. Rivendell Kütüphanesi, hem sakinleri hem de misafirleri için bir bilgelik sýðýnaðý olmayý sürdürüyor.\n\nUnutmayýn, Imladris’e gelen her yol, sizi geçmiþin sýrlarýna ve geleceðin umutlarýna götüren bir bilgelik yolculuðudur.",
                ContactInfo = "Ýletiþim: info@imladriskutuphanesi.com"
            };

            return View(model);
        }


    }
}
