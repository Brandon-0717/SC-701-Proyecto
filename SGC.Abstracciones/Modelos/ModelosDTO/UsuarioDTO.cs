
using System.ComponentModel.DataAnnotations;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class UsuarioDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El Correo Electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo Electronico no es valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El numero de telefono es obligatorio")]
        [Phone(ErrorMessage = "El numero de telefono no es valido")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [MaxLength(256, ErrorMessage = "El nombre de usuario no puede exceder los 256 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(256, ErrorMessage = "El nombre no puede exceder los 256 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [MaxLength(256, ErrorMessage = "El primer apellido no puede exceder los 256 caracteres")]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "El segundo apellido es obligatorio")]
        [MaxLength(256, ErrorMessage = "El segundo apellido no puede exceder los 256 caracteres")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "La identificacion es obligatoria")]
        [MaxLength(256, ErrorMessage = "La identificacion no puede exceder los 256 caracteres")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [MaxLength(512, ErrorMessage = "La URL de la foto de perfil no puede exceder los 512 caracteres")]
        public string? FotoPerfilUrl { get; set; }
        public Guid Estados_FK_AspNetUsers { get; set; }

        public string? NombreEstado { get; set; }
        public string? Rol { get; set; }
    }
}
