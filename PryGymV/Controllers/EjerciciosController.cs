using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PryGymV.Data;
using PryGymV.Models;

namespace PryGymV.Controllers
{
    public class EjerciciosController : Controller
    {
        private readonly PryGymVContext _context;

        public EjerciciosController(PryGymVContext context)
        {
            _context = context;
        }

        // GET: Ejercicios
        public async Task<IActionResult> Index()
        {
            var pryGymVContext = _context.Ejercicio.Include(e => e.Usuario);
            return View(await pryGymVContext.ToListAsync());
        }

        // GET: Ejercicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ejercicio == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicio
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EjercicioID == id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            return View(ejercicio);
        }

        // GET: Ejercicios/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID");
            return View();
        }

        // POST: Ejercicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EjercicioID,UsuarioID,NombreEjercicio,Repeticiones")] Ejercicio ejercicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ejercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID", ejercicio.UsuarioID);
            return View(ejercicio);
        }

        // GET: Ejercicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ejercicio == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicio.FindAsync(id);
            if (ejercicio == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID", ejercicio.UsuarioID);
            return View(ejercicio);
        }

        // POST: Ejercicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EjercicioID,UsuarioID,NombreEjercicio,Repeticiones")] Ejercicio ejercicio)
        {
            if (id != ejercicio.EjercicioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ejercicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EjercicioExists(ejercicio.EjercicioID))
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
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID", ejercicio.UsuarioID);
            return View(ejercicio);
        }

        // GET: Ejercicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ejercicio == null)
            {
                return NotFound();
            }

            var ejercicio = await _context.Ejercicio
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EjercicioID == id);
            if (ejercicio == null)
            {
                return NotFound();
            }

            return View(ejercicio);
        }

        // POST: Ejercicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ejercicio == null)
            {
                return Problem("Entity set 'PryGymVContext.Ejercicio'  is null.");
            }
            var ejercicio = await _context.Ejercicio.FindAsync(id);
            if (ejercicio != null)
            {
                _context.Ejercicio.Remove(ejercicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EjercicioExists(int id)
        {
          return _context.Ejercicio.Any(e => e.EjercicioID == id);
        }
    }
}
