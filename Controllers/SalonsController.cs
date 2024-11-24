using KuaforYonetim.Models;
using Microsoft.AspNetCore.Mvc;

public class RandevuController : Controller
{
    private readonly ApplicationDbContext _context;

    public RandevuController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Randevu oluşturma sayfasına yönlendirme
    public IActionResult Create()
    {
        ViewBag.Calisanlar = _context.Calisanlar.ToList();
        ViewBag.Hizmetler = _context.Hizmetler.ToList();
        return View();
    }

    // Randevu kaydetme
    [HttpPost]
    public IActionResult Create(Randevu randevu)
    {
        // Çalışanın ve randevu saatinin çakışıp çakışmadığını kontrol ediyoruz
        var mevcutRandevular = _context.Randevular
            .Where(r => r.CalisanId == randevu.CalisanId &&
                        r.BaslangicSaati < randevu.BitisSaati &&
                        r.BitisSaati > randevu.BaslangicSaati);

        if (mevcutRandevular.Any())
        {
            ModelState.AddModelError("", "Bu zaman diliminde çalışan meşgul.");
            return View(randevu);
        }

        _context.Randevular.Add(randevu);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
