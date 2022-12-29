using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PryGymV.Data;
using PryGymV.Models;
using System.Data;

namespace PryGymV.Controllers
{
    [Authorize(Roles = "cli,adm")]
    public class RutinaController : Controller
    {
        
        private readonly PryGymVContext _context;

        public RutinaController(PryGymVContext context)
        {

            _context = context;
        }

        public IActionResult Recomendacion()
        {
            var a = (from u in _context.Usuario
                     join e in _context.Ejercicio on u.UsuarioID equals e.UsuarioID
                     where e.Recomendado == true
                     select new Rutina
                     {
                         NombreEjercicio = e.NombreEjercicio,
                         Repeticiones = e.Repeticiones
                     }).ToList();

            return View(a);
        }
    }
}
