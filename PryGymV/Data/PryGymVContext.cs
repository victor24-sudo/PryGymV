using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PryGymV.Models;

namespace PryGymV.Data
{
    public class PryGymVContext : DbContext
    {
        public PryGymVContext (DbContextOptions<PryGymVContext> options)
            : base(options)
        {
        }

        public DbSet<PryGymV.Models.Usuario> Usuario { get; set; } = default!;

        public DbSet<PryGymV.Models.Ejercicio> Ejercicio { get; set; }

        public DbSet<PryGymV.Models.Imc> Imc { get; set; }
    }
}
