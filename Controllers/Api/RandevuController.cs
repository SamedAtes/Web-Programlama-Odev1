
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KuaforYonetim.Data;
using KuaforYonetim.Models;



[Route("api/[controller]")]
[ApiController]
public class? RandevuController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RandevuController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Tüm Randevuları Listele
    [HttpGet]
    public IActionResult GetRandevular()
    {
        var randevular = _context.Randevular
            .Include(r => r.Calisan)
            .Include(r => r.Hizmet)
            .ToList();

        return Ok(randevular);
    }

    // ID'ye Göre Tekil Randevu Getir
    [HttpGet("{id}")]
    public IActionResult GetRandevu(int id)
    {
        var randevu = _context.Randevular
            .Include(r => r.Calisan)
            .Include(r => r.Hizmet)
            .FirstOrDefault(r => r.Id == id);

        if (randevu == null)
        {
            return NotFound();
        }

        return Ok(randevu);
    }

    // Yeni Randevu Ekle
    [HttpPost]
    public IActionResult CreateRandevu([FromBody] Randevu yeniRandevu)
    {
        if (ModelState.IsValid)
        {
            _context.Randevular.Add(yeniRandevu);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRandevu), new { id = yeniRandevu.Id }, yeniRandevu);
        }

        return BadRequest(ModelState);
    }

    // Randevu Güncelle
    [HttpPut("{id}")]
public IActionResult UpdateRandevu(int id, [FromBody] Randevu guncelRandevu)
    {
        if (id != guncelRandevu.Id)
        {
            return BadRequest();
        }

        _context.Entry(guncelRandevu).State = EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Randevular.Any(r => r.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // Randevu Sil
    [HttpDelete("{id}")]
    public IActionResult DeleteRandevu(int id)
    {
        var randevu = _context.Randevular.Find(id);
        if (randevu == null)
        {
            return NotFound();
        }

        _context.Randevular.Remove(randevu);
        _context.SaveChanges();

        return NoContent();
    }
}

}
