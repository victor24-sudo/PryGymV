namespace PryGymV.Models
{
    public class Rutina
    {
        public string? Nombre { get; set; }
        public string? NombreEjercicio { get; set; }

        public string? Repeticiones { get; set; }

        public string? Estado { get; set; }

        public int EjercicioID { get; set; }

        public bool? Recomendado { get; set; }
    }
}
