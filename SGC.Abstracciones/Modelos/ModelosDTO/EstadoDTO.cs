
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class EstadoDTO
    {
        public Guid ESTADOS_PK { get; set; }

        [Required]
        [MaxLength(128, ErrorMessage = "El nombre no puede superar 128 caracteres")]
        public string Nombre_Estado { get; set; }

        // Auditoría
        [MaxLength(128, ErrorMessage = "El Creador no puede superar 128 caracteres")]
        public string? CreadoPor { get; set; }

        [MaxLength(128, ErrorMessage = "El Modificacdor no puede superar 128 caracteres")]
        public string? ModificadoPor { get; set; }

        public DateTime? Fecha_Modificacion { get; set; }
    }
}
