# **Imladris Kütüphanesi Yönetim Sistemi**

**Imladris Kütüphanesi Yönetim Sistemi**, ASP.NET Core MVC kullanılarak geliştirilmiş bir dijital kütüphane yönetim sistemidir. Bu sistemde kullanıcılar giriş yaparak kitaplar ve yazarlar için ekleme, düzenleme, silme ve görüntüleme işlemi yapabilirler. Projede veri tabanı kullanılmamış olup, tüm veriler statik listeler aracılığıyla yönetilmektedir.

## **İçindekiler**

1. [Proje Hakkında](#proje-hakkında)
2. [Özellikler](#özellikler)
3. [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
4. [Proje Yapısı](#proje-yapısı)
    - [Controllers](#controllers)
    - [Models](#models)
    - [Services](#services)
    - [ViewModels](#viewmodels)
    - [Views](#views)
5. [Veri Yönetimi (Statik Listeler)](#veri-yonetimi)
6. [CSS ve Statik Dosyalar](#css-ve-statik-dosyalar)
7. [Projeyi Test Etme](#projeyi-test-etme)

---

## **Proje Hakkında**

**Imladris Kütüphanesi Yönetim Sistemi**, sisteme giriş yapan kullanıcıların kütüphanedeki kitaplar ve yazarlarla ilgili bilgi almasına ve  kitap/yazar ekleme, düzenleme ve silme işlemlerini yapabilmesine olanak tanır. 
---

## **Özellikler**

- **Kullanıcı Yönetimi**: Üyelik sistemi ile kullanıcılar kayıt olabilir ve giriş yapabilir.
- **Kitap Yönetimi**: Kitap ekleme, düzenleme, silme ve kitap detaylarını görüntüleme.
- **Yazar Yönetimi**: Yazar ekleme, düzenleme, silme ve yazar detaylarını görüntüleme.
- **Statik Veri Saklama**: Veriler bir veri tabanı yerine, statik listeler ile bellekte saklanır.
- **Responsive Tasarım**: **Bootstrap 5** ile mobil uyumlu bir arayüz.

---

## **Kullanılan Teknolojiler**

- **ASP.NET Core MVC 6.0**: Backend geliştirme framework’ü.
- **C#**: Backend programlama dili.
- **HTML5/CSS3**: Ön yüz geliştirme için temel teknolojiler.
- **Bootstrap 5**: Responsive ve şık kullanıcı arayüzü tasarımı.
- **Statik Listeler**: Veri saklama için kullanılan yapılar.
- **Git**: Versiyon kontrol sistemi.

---

## **Proje Yapısı**

### **1. Controllers**

Controller'lar, MVC yapısındaki iş mantığını kontrol eden bileşenlerdir. Projede 4 ana controller bulunmaktadır:

- **AuthController**:
  - Kullanıcı kayıt, giriş ve çıkış işlemleri.
  - View’lar: `Login.cshtml`, `SignUp.cshtml`, `Welcome.cshtml`.
  
- **AuthorController**:
  - Yazar ekleme, düzenleme, silme, listeleme ve detay gösterme işlemleri.
  - View’lar: `Create.cshtml`, `Edit.cshtml`, `Delete.cshtml`, `Details.cshtml`, `List.cshtml`.

- **BookController**:
  - Kitap ekleme, düzenleme, silme, listeleme ve detay gösterme işlemleri.
  - View’lar: `Create.cshtml`, `Edit.cshtml`, `Delete.cshtml`, `Details.cshtml`, `List.cshtml`.

- **HomeController**:
  - Ana sayfa ve hakkında sayfası.
  - View’lar: `Index.cshtml`, `About.cshtml`.

### **2. Models**

Proje boyunca kullanılan temel veri modelleri şunlardır:

- **User**: Kullanıcı bilgilerini tutar.
  - `Id`: Kullanıcının benzersiz kimliği.
  - `FullName`: Kullanıcının tam adı.
  - `Email`: E-posta adresi.
  - `Password`: Şifre bilgisi.
  - `PhoneNumber`: Telefon numarası.
  - `JoinDate`: Sisteme kayıt olduğu tarih.

- **Author**: Yazar bilgilerini tutar.
  - `Id`: Yazarın benzersiz kimliği.
  - `FirstName`: Yazarın adı.
  - `LastName`: Yazarın soyadı.
  - `DateOfBirth`: Yazarın doğum tarihi.
  - `FullName`: Yazarın adı ve soyadı.
  - `IsDeleted`: Silinmiş yazarları işaretlemek için.

- **Book**: Kitap bilgilerini tutar.
  - `Id`: Kitabın benzersiz kimliği.
  - `Title`: Kitabın başlığı.
  - `AuthorId`: Kitabın yazarının kimliği.
  - `Genre`: Kitabın türü.
  - `PublishDate`: Kitabın yayın tarihi.
  - `CopiesAvailable`: Kitabın mevcut kopya sayısı.
  - `ISBN`: Kitabın ISBN numarası.
  - `ImageUrl`: Kapak resmi.
  - `IsDeleted`: Silinmiş kitapları işaretlemek için.

### **3. Services**

Servis katmanı, uygulama mantığını soyutlayarak veri işlemlerini controller'lar ile model katmanı arasında yönetir.

- **AuthorService**: Yazar statik listelerle çalışarak yazar ekleme ve listeleme işlevlerini gerçekleştirir.
- **IAuthorService**: Yazar servisi için kullanılan arayüz. Bu servis, katmanlı mimaride bağımlılığı azaltmak için kullanılır.

### **4. ViewModels**

ViewModel'lar, controller ile view'lar arasında veri taşımak için kullanılır. Projede 8 ViewModel bulunmaktadır:

- **AboutViewModel**: Hakkında sayfasında kullanılan bilgiler.
- **AuthorDetailsViewModel**: Yazar detaylarını ve yazara ait kitapların listesini taşır.
- **AuthorViewModel**: Yazar ekleme, düzenleme ve listeleme işlemlerinde kullanılır.
- **BookCreateViewModel**: Kitap oluşturma işlemlerinde kullanılır.
- **BookEditViewModel**: Kitap düzenleme işlemlerinde kullanılır.
- **BookViewModel**: Kitap listesi ve kitap detayları için kullanılır.
- **LoginViewModel**: Kullanıcı giriş formu için kullanılır.
- **SignUpViewModel**: Kullanıcı kayıt formu için kullanılır.

### **5. Views**

Razor View'ları, kullanıcılara sunulan sayfaların oluşturulmasında kullanılır. Proje aşağıdaki gibi yapılandırılmıştır:

- **Auth**: Kullanıcı giriş ve kayıt işlemleri için view'lar.
  - `Login.cshtml`: Kullanıcı giriş formu.
  - `SignUp.cshtml`: Kullanıcı kayıt formu.
  - `Welcome.cshtml`: Giriş yaptıktan sonra kullanıcıya hoş geldiniz mesajı gösterir.
  
- **Author**: Yazar yönetimi ile ilgili view'lar.
  - `Create.cshtml`: Yeni yazar ekleme formu.
  - `Delete.cshtml`: Yazar silme işlemi onay sayfası.
  - `Details.cshtml`: Yazarın detaylarını ve yazara ait kitapların listesini gösterir.
  - `Edit.cshtml`: Yazar düzenleme formu.
  - `List.cshtml`: Tüm yazarları listeleme sayfası.

- **Book**: Kitap yönetimi ile ilgili view'lar.
  - `Create.cshtml`: Yeni kitap ekleme formu.
  - `Delete.cshtml`: Kitap silme işlemi onay sayfası.
  - `Details.cshtml`: Kitabın detaylarını gösterir.
  - `Edit.cshtml`: Kitap düzenleme formu.
  - `List.cshtml`: Tüm kitapları listeleme sayfası.

- **Home**: Ana sayfa ve hakkında sayfası.
  - `Index.cshtml`: Ana sayfa.
  - `About.cshtml`: Kütüphane hakkında bilgiler.

- **Shared**: Ortak kullanılan view dosyaları.
  - `_ViewImports.cshtml`: Razor view'ları için kullanılan namespace'leri içeren dosya.
  - `_ViewStart.cshtml`: Tüm view'lar için başlangıç ayarlarını içeren dosya.

---

## **Veri Yönetimi**

- Proje veri tabanı kullanmamakta olup, veriler statik listeler aracılığıyla yönetilmektedir. 
---

## **CSS ve Statik Dosyalar**

Projedeki statik dosyalar, **wwwroot** klasörü altında tutulmaktadır:

- **CSS Dosyaları**: `homecss` ve `sitecss` dosyaları genel stil ve ana sayfa için özel stil ayarlarını içerir.
- **Görseller**: Kitap kapak resimleri ve diğer görseller, `images` klasörü altında saklanmaktadır.

---

## **Projeyi Test Etme**

Proje için herhangi bir test framework'ü kullanılmamaktadır. Ancak, uygulama statik listeler üzerinden çalıştığı için test işlemleri manuel olarak gerçekleştirilebilir. Sisteme giriş yaptıktan sonra kitap ve yazar ekleme, düzenleme, silme işlemlerini deneyerek uygulamanın doğruluğunu test edebilirsiniz.
