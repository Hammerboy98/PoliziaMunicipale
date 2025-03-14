using Microsoft.AspNetCore.Mvc;
using PoliziaMunicipale2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PoliziaMunicipale2.Data;

namespace PoliziaMunicipale2.Controllers
{
    public class VerbaleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VerbaleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Crea un nuovo verbale
        public IActionResult Create()
        {
            ViewData["Idanagrafica"] = new SelectList(_context.Anagraficas, "Idanagrafica", "Cognome");
            ViewData["Idviolazione"] = new SelectList(_context.TipoViolaziones, "Idviolazione", "Descrizione");
            return View();
        }

        // POST: Crea un nuovo verbale
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idverbale,Idanagrafica,Idviolazione,DataViolazione,Importo,DecurtamentoPunti")] Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verbale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idanagrafica"] = new SelectList(_context.Anagraficas, "Idanagrafica", "Cognome", verbale.Idanagrafica);
            ViewData["Idviolazione"] = new SelectList(_context.TipoViolaziones, "Idviolazione", "Descrizione", verbale.Idviolazione);
            return View(verbale);
        }
        public async Task<IActionResult> Index()
        {
            var verbali = await _context.Verbales
                .Include(v => v.IdanagraficaNavigation)
                .Include(v => v.IdviolazioneNavigation)
                .ToListAsync();
            return View(verbali);
        }
    }
}
