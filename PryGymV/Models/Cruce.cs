using System.ComponentModel.DataAnnotations;

namespace PryGymV.Models
{
    public class Cruce
    {
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public float Peso { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public float? Resultado { get; set; }

        public string? Estado { get; set; }

        public int? UsuarioID {get; set; }


}
}
