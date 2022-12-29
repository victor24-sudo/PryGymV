using System.ComponentModel.DataAnnotations;

namespace PryGymV.Models
{
    public class Imc
    {
        public int ImcID { get; set; }

        public int UsuarioID { get; set; }

        public float Estatura { get; set; }

        public float Peso { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        public float? Resultado { get; set; }

        public string? Estado { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
