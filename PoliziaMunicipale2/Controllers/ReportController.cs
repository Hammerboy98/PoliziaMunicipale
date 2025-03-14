using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoliziaMunicipale2.Data;
using PoliziaMunicipale2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PoliziaMunicipale2.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Report 1: Totale dei verbali trascritti raggruppati per trasgressore
        public async Task<IActionResult> VerbaliPerTrasgressore()
        {
            var verbaliPerTrasgressore = await _context.Verbales
                .GroupBy(v => v.IdanagraficaNavigation.Cognome + " " + v.IdanagraficaNavigation.Nome)
                .Select(g => new
                {
                    Trasgressore = g.Key,
                    TotaleVerbali = g.Count()
                })
                .ToListAsync();

            return View(verbaliPerTrasgressore);
        }

        // Report 2: Totale dei punti decurtati raggruppati per trasgressore
        public async Task<IActionResult> PuntiDecurtatiPerTrasgressore()
        {
            var puntiDecurtatiPerTrasgressore = await _context.Verbales
                .GroupBy(v => v.IdanagraficaNavigation.Cognome + " " + v.IdanagraficaNavigation.Nome)
                .Select(g => new
                {
                    Trasgressore = g.Key,
                    TotalePuntiDecurtati = g.Sum(v => v.DecurtamentoPunti)
                })
                .ToListAsync();

            return View(puntiDecurtatiPerTrasgressore);
        }

        // Report 3: Violazioni con più di 10 punti
        public async Task<IActionResult> ViolazioniGrandiPunti()
        {
            var violazioniGrandiPunti = await _context.Verbales
                .Where(v => v.DecurtamentoPunti > 10)
                .Select(v => new
                {
                    v.IdanagraficaNavigation.Cognome,
                    v.IdanagraficaNavigation.Nome,
                    v.DataViolazione,
                    v.DecurtamentoPunti,
                    v.Importo
                })
                .ToListAsync();

            return View(violazioniGrandiPunti);
        }

        // Report 4: Violazioni con importo maggiore di 400 euro
        public async Task<IActionResult> ViolazioniOltre400Euro()
        {
            var violazioniOltre400Euro = await _context.Verbales
                .Where(v => v.Importo > 400)
                .Select(v => new
                {
                    v.IdanagraficaNavigation.Cognome,
                    v.IdanagraficaNavigation.Nome,
                    v.DataViolazione,
                    v.Importo,
                    v.DecurtamentoPunti
                })
                .ToListAsync();

            return View(violazioniOltre400Euro);
        }
    }
}
