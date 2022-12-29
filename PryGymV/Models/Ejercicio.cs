namespace PryGymV.Models
{
    public class Ejercicio
    {
        public int EjercicioID { get; set; }

        public int UsuarioID { get; set; }

        public string? NombreEjercicio { get; set; }

        public string? Repeticiones { get; set; }

        public virtual Usuario? Usuario { get; set; }

        public bool? Recomendado { get; set; }
    }
}
