
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.Abstracciones.Modelos.ModeloDA
{
    [Table("ESTADOS")]
    public class EstadoDA
    {
        [Key]
        [Column("ESTADOS_PK")]
        public Guid ESTADOS_PK { get; set; }

        [Required]
        [MaxLength(128)]
        [Column("Nombre_Estado")]
        public string Nombre_Estado { get; set; }

        // Auditoría
        [MaxLength(128)]
        [Column("CreadoPor")]
        public string CreadoPor { get; set; }

        [MaxLength(128)]
        [Column("ModificadoPor")]
        public string ModificadoPor { get; set; }

        [Column("Fecha_Modificacion")]
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
