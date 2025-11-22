
using System.ComponentModel.DataAnnotations;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class RolDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [MaxLength(256, ErrorMessage = "El nombre del rol no puede exceder los 256 caracteres")]
        [Display(Name = "Nombre del Rol")]
        public string Name { get; set; }
        public string? NormalizedName { get; set; }
    }
}
