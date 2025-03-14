using Microsoft.AspNetCore.Mvc;
using PoliziaMunicipale2.Models;
using Microsoft.EntityFrameworkCore;
using PoliziaMunicipale2.Data;

namespace PoliziaMunicipale2.Controllers
{
    public class TipoViolazioneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoViolazioneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Visualizza tutte le violazioni
        public async Task<IActionResult> Index()
        {
            var violazioni = await _context.TipoViolaziones.ToListAsync();
            return View(violazioni);
        }

        // Crea una nuova violazione
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crea una nuova violazione
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idviolazione,Descrizione,DecurtamentoPunti,Importo")] TipoViolazione violazione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(violazione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(violazione);
        }
    }
}
