
using System.ComponentModel.DataAnnotations;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class UsuarioDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El Correo Electronico es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo Electronico no es valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contrasenia es obligatoria")]
        [MinLength(6, ErrorMessage = "La contrasenia debe tener al menos 6 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,12}$",
        ErrorMessage = "La contraseña debe tener entre 8 y 12 caracteres, sin espacios, con al menos una mayúscula, una minúscula y un número.")]
        public string Contrasenia { get; set; }

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
        public List<RolDTO>? Roles { get; set; }
    }

    public class UsuarioRegistroModelDTO
    {
        [Required(ErrorMessage = "La identificación es obligatoria")]
        [MaxLength(256, ErrorMessage = "La identificación no puede exceder los 256 caracteres")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(256, ErrorMessage = "El nombre no puede exceder los 256 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,12}$",
            ErrorMessage = "La contraseña debe tener entre 8 y 12 caracteres, sin espacios, con al menos una mayúscula, una minúscula y un número.")]
        public string Contrasenia { get; set; }

        [Required(ErrorMessage = "La confirmación de la contraseña es obligatoria")]
        [Compare("Contrasenia", ErrorMessage = "La contraseña y la confirmación de la contraseña no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmarContrasenia { get; set; }
    }
}
