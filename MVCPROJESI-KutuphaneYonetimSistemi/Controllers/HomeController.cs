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
                Title = "Rivendell (Imladris) K�t�phanesi: Bilgeli�in ve Bilimin Ba�kenti",
                Description = "Orta D�nya'n�n kalbinde, esrarengiz da�lar�n aras�nda sakl� bir vaha olarak bilinen Rivendell, ya da elflerin diliyle 'Imladris', sadece bir s���nak de�il, ayn� zamanda y�zy�llard�r bilgelik ve ��renmenin de merkezi olmu�tur. Imladris K�t�phanesi, bu k�kl� miras� ya�atmak ve Orta D�nya'n�n d�rt bir yan�ndan gelen bilgi aray���ndaki ruhlara ���k tutmak amac�yla kurulmu�tur.\n\nBu k�t�phanede, elflerin efsanevi �iirlerinden, insanlar ve c�celerin tarihi destanlar�na; hobbitlerin m�tevaz� ya�am �yk�lerinden, do�unun esrarengiz halklar�n�n kadim bilgilerine kadar say�s�z eser yer al�r. Her bir kitap, b�y�l� sayfalar�nda sonsuz bir bilgi deryas� sunar. K�t�phanemiz, sadece kitaplardan ibaret de�il; burada el yazmalar�, nadir haritalar ve Orta D�nya�n�n kay�p halklar�na dair unutulmu� belgeler de saklanmaktad�r.\n\nK�t�phane, Lord Elrond'un rehberli�inde, hem ge�mi�i ara�t�rmak isteyen bilginlere hem de gelece�i in�a etmek isteyen cesur ruhlara ev sahipli�i yapar. Buraya ad�m atan her ziyaret�i, kadim bilgeli�in izini s�rebilecek, ��renme yolculu�unda e�siz bir deneyim ya�ayacakt�r. Rivendell K�t�phanesi, hem sakinleri hem de misafirleri i�in bir bilgelik s���na�� olmay� s�rd�r�yor.\n\nUnutmay�n, Imladris�e gelen her yol, sizi ge�mi�in s�rlar�na ve gelece�in umutlar�na g�t�ren bir bilgelik yolculu�udur.",
                ContactInfo = "�leti�im: info@imladriskutuphanesi.com"
            };

            return View(model);
        }


    }
}
