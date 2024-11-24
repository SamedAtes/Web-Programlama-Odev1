using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using KuaforYonetim.Data; // ApplicationDbContext namespace'i burada olmalı
using KuaforYonetim.Models;

namespace KuaforYonetim.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor: ApplicationDbContext bağımlılığı enjekte edilir
        public RandevuController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: Randevu (Tüm randevuları listeler)
        public async Task<IActionResult> Index()
        {
            // Randevuları ilişkili çalışan ve hizmetlerle birlikte çekiyoruz
            var randevular = await _context.Randevular
                .Include(r => r.Calisan)
                .Include(r => r.Hizmet)
                .ToListAsync();
            return View(randevular);
        }

        // GET: Randevu/Create (Yeni randevu oluşturma sayfasını gösterir)
        public IActionResult Create()
        {
            // Çalışanlar ve Hizmetler için DropDown listeleri oluşturuluyor
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "AdSoyad");
            ViewData["HizmetId"] = new SelectList(_context.Hizmetler, "HizmetId", "HizmetAdi");
            return View();
        }

        // POST: Randevu/Create (Yeni bir randevu oluşturur)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RandevuId,CalisanId,HizmetId,BaslangicSaati,BitisSaati,Ucret")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni randevu ekleniyor
                    _context.Add(randevu);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Hata yönetimi
                    ModelState.AddModelError("", $"Randevu kaydedilirken bir hata oluştu: {ex.Message}");
                }
            }

            // Hata durumunda, ViewData tekrar dolduruluyor
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "CalisanId", "AdSoyad", randevu.CalisanId);
            ViewData["HizmetId"] = new SelectList(_context.Hizmetler, "HizmetId", "HizmetAdi", randevu.HizmetId);
            return View(randevu);
        }
    }
}
