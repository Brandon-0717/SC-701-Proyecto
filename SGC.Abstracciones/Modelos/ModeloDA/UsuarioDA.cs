
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SGC.Abstracciones.Modelos.ModeloDA
{
    public class UsuarioDA : IdentityUser
    {
        [Required, MaxLength(256)]
        public string Nombre { get; set; }

        [Required, MaxLength(256)]
        public string PrimerApellido { get; set; }

        [Required, MaxLength(256)]
        public string SegundoApellido { get; set; }

        [Required, MaxLength(256)]
        public string Identificacion { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(512)]
        public string FotoPerfilUrl { get; set; } = "/img/avatars/default.png";

        [Required]
        public Guid Estados_FK_AspNetUsers { get; set; }
    }
}