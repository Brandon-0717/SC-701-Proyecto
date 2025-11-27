
using Microsoft.AspNetCore.Identity;

namespace SGC.Abstracciones.Modelos.ModeloDA
{
    public class UsuarioDA : IdentityUser
    {
        public string Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string Identificacion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? FotoPerfilUrl { get; set; }
        public Guid? Estados_FK_AspNetUsers { get; set; }
    }
}