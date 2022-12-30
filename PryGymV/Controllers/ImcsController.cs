using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PryGymV.Data;
using PryGymV.Models;

namespace PryGymV.Controllers
{
    
    public class ImcsController : Controller
    {
        private readonly PryGymVContext _context;

        public ImcsController(PryGymVContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "cli, adm")]

        // GET: Imcs
        public async Task<IActionResult> Index()
        {
            var a = HttpContext.User.IsInRole("adm");
            var c = HttpContext.User.Identity.Name;

            if (a==true)
            {
                var pryGymVContext = _context.Imc.Include(i => i.Usuario);
                return View(await pryGymVContext.ToListAsync());
            }
            else
            {
                var b = (from u in _context.Usuario
                         join i in _context.Imc on u.UsuarioID equals i.UsuarioID
                         where u.Email == c
                         select i
                         );

                return View(await b.ToListAsync());
            }

            
        }

        [Authorize(Roles = "cli, adm")]

        // GET: Imcs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Imc == null)
            {
                return NotFound();
            }

            var imc = await _context.Imc
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.ImcID == id);
            if (imc == null)
            {
                return NotFound();
            }

            return View(imc);
        }

        [Authorize(Roles = "cli, adm")]

        // GET: Imcs/Create
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

        // POST: Imcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImcID,UsuarioID,Estatura,Peso,Fecha,Resultado,Estado")] Imc imc)
        { 
            var a = HttpContext.User.IsInRole("adm");
            var c = HttpContext.User.Identity.Name;

            if (a == true)
            {

                float est = imc.Estatura;
                float est1 = est / 100;
                float pes = imc.Peso;
                double resul = (float)(pes / Math.Pow(est1, 2));

                imc.Resultado = (float)resul;

                imc.Estado = Estado((float)resul);
                if (ModelState.IsValid)
                {
                    _context.Add(imc);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID", imc.UsuarioID);
                return View(imc);
            }
            else
            {
                float est = imc.Estatura;
                float pes = imc.Peso;
                float est1 = est / 100;
                double resul = (float)(pes / Math.Pow(est1, 2));

                imc.Resultado = (float)resul;

                imc.Estado = Estado((float)resul);

                var b = (from u in _context.Usuario
                         where u.Email == c
                         select u).FirstOrDefault();
                imc.UsuarioID = b.UsuarioID;

                _context.Add(imc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        public string Estado(float resul)
        {

            if (resul < 18.5)
            {
                return "Bajo Peso";

            }
            else if (resul >= 18.5 && resul <= 24.9)
            {
                return "Peso Saludable";

            }
            else if (resul >= 25.0 && resul <= 29.9)
            {
                return "SobrePeso";
            }
            else
            {
                return "Obesidad";
            }

        }

        [Authorize(Roles = "cli, adm")]

        // GET: Imcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Imc == null)
            {
                return NotFound();
            }

            var imc = await _context.Imc.FindAsync(id);
            if (imc == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID", imc.UsuarioID);
            return View(imc);
        }


        [Authorize(Roles = "cli, adm")]

        // POST: Imcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImcID,UsuarioID,Estatura,Peso,Fecha,Resultado,Estado")] Imc imc)
        {
            if (id != imc.ImcID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImcExists(imc.ImcID))
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
            ViewData["UsuarioID"] = new SelectList(_context.Usuario, "UsuarioID", "UsuarioID", imc.UsuarioID);
            return View(imc);
        }

        [Authorize(Roles = "cli, adm")]

        // GET: Imcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Imc == null)
            {
                return NotFound();
            }

            var imc = await _context.Imc
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.ImcID == id);
            if (imc == null)
            {
                return NotFound();
            }

            return View(imc);
        }

        [Authorize(Roles = "cli, adm")]
        // POST: Imcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Imc == null)
            {
                return Problem("Entity set 'PryGymVContext.Imc'  is null.");
            }
            var imc = await _context.Imc.FindAsync(id);
            if (imc != null)
            {
                _context.Imc.Remove(imc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "adm")]

        public async Task<IActionResult> Cruce (string buscar, DateTime fechaini, DateTime fechafin)
        {

            var a = (from u in _context.Usuario
                     join i in _context.Imc on u.UsuarioID equals i.UsuarioID
                     where  (i.Fecha >= fechaini && i.Fecha <= fechafin) || (u.Nombre == buscar)
                     select new Cruce
                     {
                         UsuarioID = u.UsuarioID,
                         Nombre = u.Nombre,
                         Apellido = u.Apellido,
                         Peso = i.Peso,
                         Fecha = i.Fecha,
                         Resultado = i.Resultado,
                         Estado = i.Estado
                     }).OrderBy(u => u.Resultado).ToList();

            return View(a);
        }

        [Authorize(Roles = "adm")]

        public async Task<IActionResult> Rutina(int id)
        {

            var a = (from u in _context.Usuario
                     join e in _context.Ejercicio on u.UsuarioID equals e.UsuarioID
                     join i in _context.Imc on u.UsuarioID equals i.UsuarioID
                     where e.UsuarioID == id
                     select new Rutina
                     {
                         Nombre = u.Nombre,
                         NombreEjercicio = e.NombreEjercicio,
                         Repeticiones = e.Repeticiones,
                         Estado = i.Estado,
                         EjercicioID = e.EjercicioID,
                         Recomendado = e.Recomendado
                     }).ToList();

            return View(a);
        }

        [Authorize(Roles = "adm")]

        [HttpPost]
        public async Task<IActionResult> Agregar(int id)
        {

            var a = _context.Ejercicio.Where(e  => e.EjercicioID == id).FirstOrDefault();

            a.Recomendado = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Cruce");
        }

        public async Task<IActionResult> Ranking(int id,string buscar, DateTime fechaini, DateTime fechafin)
        {

            var a = (from u in _context.Usuario
                     join i in _context.Imc on u.UsuarioID equals i.UsuarioID
                     join e in _context.Ejercicio on u.UsuarioID equals e.UsuarioID
                     where (i.Fecha >= fechaini && i.Fecha <= fechafin) || (e.NombreEjercicio == buscar)
                     select new Ranking
                     {
                         UsuarioID = u.UsuarioID,
                         Nombre = u.Nombre,
                         Apellido = u.Apellido,
                         Peso = i.Peso,
                         Fecha = i.Fecha,
                         Resultado = i.Resultado,
                         Estado = i.Estado,
                         Numero = e.Numero,
                         NombreEjercicio = e.NombreEjercicio

                     }).OrderBy(u => u.Resultado).ToList();

            return View(a);
        }

        public async Task<IActionResult> Valor(string buscar, int id)
        {

            var a = _context.Ejercicio.Where(e => e.NombreEjercicio == buscar).FirstOrDefault();

            a.Numero = a.Numero + 1;
            await _context.SaveChangesAsync();

            return RedirectToAction("Ranking");

        }

        public async Task<IActionResult> Numero(string buscar, int id)
        {

            var b = (from u in _context.Usuario
                     join e in _context.Ejercicio on u.UsuarioID equals e.UsuarioID
                     where e.UsuarioID == id
                     select new Numero
                     { 
                         NombreEjercicio = e.NombreEjercicio,
                         Valor = 2
                         
                     }).ToList();

            return View(b);

            
        }




        private bool ImcExists(int id)
        {
            return _context.Imc.Any(e => e.ImcID == id);
        }
    }
}
