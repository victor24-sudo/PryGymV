namespace PryGymV.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? Clave { get; set; }

        public string? Rol { get; set; }

        public virtual ICollection<Ejercicio>? Ejercicios { get; set; }

        public virtual ICollection<Imc>? Imcs { get; set; }
    }
}
