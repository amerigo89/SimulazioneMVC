using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimulazioneMVC.Model;
using SimulazioneMVC.Model.ViewModels;

namespace SimulazioneMVC.Controllers
{
    public class PresentazionisController : Controller
    {
        private readonly AutoriContext _context;

        public PresentazionisController(AutoriContext context)
        {
            _context = context;
        }

        // GET: Presentazionis
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Autori = _context.Autori.ToList();

            if (id > 0)
            {
                return View(SearchByAutoreId(id));
            }
                
            else
                return View(await _context.Presentazioni.ToListAsync());
        }

        public IEnumerable<Presentazioni> SearchByAutoreId(int id)
        {
            List<Registrazioni> regTrovate = new List<Registrazioni>();
            regTrovate = _context.Registrazioni.Where(r => r.IdAutore == id).ToList();
            List<Presentazioni> presTrovate = new List<Presentazioni>();
            foreach(var reg in regTrovate)
            {
                presTrovate.Add(_context.Presentazioni.FirstOrDefault(p => p.Id == reg.IdPresentazione));
            }
            return presTrovate;
        }

        // GET: Presentazionis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentazioni = await _context.Presentazioni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presentazioni == null)
            {
                return NotFound();
            }

            return View(presentazioni);
        }

        // GET: Presentazionis/Create
        public IActionResult Create()
        {
            PresentazioniViewModel presViewModel = new PresentazioniViewModel();
            var autori = _context.Autori.ToList();
            foreach (var i in autori)
            {
                presViewModel.Autori.Add(i);
            }
            return View(presViewModel);
        }

        // POST: Presentazionis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PresentazioniViewModel presViewModel)
        {
            if (ModelState.IsValid)
            {
                Presentazioni presentazione = new Presentazioni()
                {
                    Titolo = presViewModel.Titolo,
                    DataInizio = presViewModel.DataInizio,
                    DataFine = presViewModel.DataFine,
                    Livello = presViewModel.Livello
                };
                _context.Add(presentazione);
                await _context.SaveChangesAsync();
                int idPresCreato = presentazione.Id;
                foreach(var id in presViewModel.IdAutore)
                {
                    Registrazioni registrazione = new Registrazioni()
                    {
                        IdPresentazione = idPresCreato,
                        IdAutore = id
                    };
                    _context.Registrazioni.Add(registrazione);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(presViewModel);
        }

        // GET: Presentazionis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentazioni = await _context.Presentazioni.FindAsync(id);
            if (presentazioni == null)
            {
                return NotFound();
            }
            return View(presentazioni);
        }

        // POST: Presentazionis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titolo,DataInizio,DataFine,Livello")] Presentazioni presentazioni)
        {
            if (id != presentazioni.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presentazioni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentazioniExists(presentazioni.Id))
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
            return View(presentazioni);
        }

        // GET: Presentazionis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentazioni = await _context.Presentazioni
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presentazioni == null)
            {
                return NotFound();
            }

            return View(presentazioni);
        }

        // POST: Presentazionis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presentazioni = await _context.Presentazioni.FindAsync(id);
            _context.Presentazioni.Remove(presentazioni);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresentazioniExists(int id)
        {
            return _context.Presentazioni.Any(e => e.Id == id);
        }
    }
}
