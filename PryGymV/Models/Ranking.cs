using System.ComponentModel.DataAnnotations;

namespace PryGymV.Models
{
    public class Ranking
    {
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public float Peso { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public float? Resultado { get; set; }

        public string? Estado { get; set; }

        public int? UsuarioID { get; set; }

        public int? Numero { get; set; }
        public string? NombreEjercicio { get; set; }

    }
}
