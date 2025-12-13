using System.ComponentModel.DataAnnotations;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class UsuarioDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El Correo Electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El Correo Electrónico no es válido")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,12}$",
            ErrorMessage = "La contraseña debe tener entre 8 y 12 caracteres, sin espacios, con al menos una mayúscula, una minúscula y un número.")]
        public string Contrasenia { get; set; }

        //[Required(ErrorMessage = "El número de teléfono es obligatorio")]
        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono { get; set; }

        //[Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [MaxLength(256, ErrorMessage = "El nombre de usuario no puede exceder los 256 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(256, ErrorMessage = "El nombre no puede exceder los 256 caracteres")]
        public string Nombre { get; set; }

        [MaxLength(256, ErrorMessage = "El primer apellido no puede exceder los 256 caracteres")]
        public string PrimerApellido { get; set; }

        [MaxLength(256, ErrorMessage = "El segundo apellido no puede exceder los 256 caracteres")]
        public string SegundoApellido { get; set; }

        [MaxLength(12, ErrorMessage = "La identificación no puede exceder los 12 caracteres")]
        public string Identificacion { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        [MaxLength(512, ErrorMessage = "La URL de la foto de perfil no puede exceder los 512 caracteres")]
        public string? FotoPerfilUrl { get; set; }

        public Guid? Estados_FK_AspNetUsers { get; set; }

        public string? NombreEstado { get; set; }

        public List<RolDTO>? Roles { get; set; }
        public List<string>? NombreRoles { get; set; }

        public bool EmailConfirmed { get; set; }
    }

    public class UsuarioRegistroModelDTO
    {
        [Required(ErrorMessage = "La identificación es obligatoria")]
        [MinLength(9, ErrorMessage = "La identificación debe tener almenos 9 caracteres")]
        [MaxLength(12, ErrorMessage = "La identificación no puede exceder los 256 caracteres")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(30, ErrorMessage = "El nombre no puede exceder los 256 caracteres")]
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