using Microsoft.AspNetCore.Mvc;
using PoliziaMunicipale2.Models;
using Microsoft.EntityFrameworkCore;
using PoliziaMunicipale2.Data;

namespace PoliziaMunicipale2.Controllers
{
    public class AnagraficaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnagraficaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Visualizza tutti i trasgressori
        public async Task<IActionResult> Index()
        {
            var trasgressori = await _context.Anagraficas.ToListAsync();
            return View(trasgressori);
        }

        // Crea un nuovo trasgressore
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crea un nuovo trasgressore
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idanagrafica,Cognome,Nome,Indirizzo,Telefono")] Anagrafica trasgressore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trasgressore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trasgressore);
        }

        // Modifica un trasgressore esistente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trasgressore = await _context.Anagraficas.FindAsync(id);
            if (trasgressore == null)
            {
                return NotFound();
            }
            return View(trasgressore);
        }

        // POST: Modifica un trasgressore esistente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idanagrafica,Cognome,Nome,Indirizzo,Telefono")] Anagrafica trasgressore)
        {
            if (id != trasgressore.Idanagrafica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trasgressore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Anagraficas.Any(e => e.Idanagrafica == trasgressore.Idanagrafica))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trasgressore);
        }

        // Elimina un trasgressore
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trasgressore = await _context.Anagraficas
                .FirstOrDefaultAsync(m => m.Idanagrafica == id);
            if (trasgressore == null)
            {
                return NotFound();
            }

            return View(trasgressore);
        }

        // POST: Elimina un trasgressore
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trasgressore = await _context.Anagraficas.FindAsync(id);
            _context.Anagraficas.Remove(trasgressore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
