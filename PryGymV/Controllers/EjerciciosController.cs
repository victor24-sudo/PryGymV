using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PryGymV.Data;
using PryGymV.Models;

namespace PryGymV.Controllers
{
    [Authorize(Roles = "cli, adm")]




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

            var a = HttpContext.User.IsInRole("adm");
            var c = HttpContext.User.Identity.Name;

            if (a == true)
            {
                var pryGymVContext = _context.Ejercicio.Include(e => e.Usuario);
                return View(await pryGymVContext.ToListAsync());
            }
            else
            {
                var b = (from u in _context.Usuario
                         join e in _context.Ejercicio on u.UsuarioID equals e.UsuarioID
                         where u.Email == c
                         select e
                         );

                return View(await b.ToListAsync());
            }

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
            var a = HttpContext.User.IsInRole("adm");
            var c = HttpContext.User.Identity.Name;

            if (a == true)
            {
                ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "Nombre");
            }
            return View();


        }

        // POST: Ejercicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EjercicioID,UsuarioID,NombreEjercicio,Repeticiones")] Ejercicio ejercicio)
        {

            var a = HttpContext.User.IsInRole("adm");
            var c = HttpContext.User.Identity.Name;
            if (a == true)

            {

                if (ModelState.IsValid)
                {
                    _context.Add(ejercicio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID", ejercicio.UsuarioID);
                return View(ejercicio);
            }else
            {
                var b = (from u in _context.Usuario
                        where u.Email == c
                        select u).FirstOrDefault();
                ejercicio.UsuarioID = b.UsuarioID;

                _context.Add(ejercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
