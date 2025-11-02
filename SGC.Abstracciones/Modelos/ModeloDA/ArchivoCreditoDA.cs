
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.Abstracciones.Modelos.ModeloDA
{
    [Table("ARCHIVOS_CREDITO")]
    public class ArchivoCreditoDA
    {
        [Key]
        [Column("ARCHIVOS_CREDITO_PK")]
        public Guid ARCHIVOS_CREDITO_PK { get; set; }

        [Column("SOLICITUDES_CREDITO_FK_ARCHIVOS_CREDITO")]
        public Guid SOLICITUDES_CREDITO_FK_ARCHIVOS_CREDITO { get; set; }

        [Column("UserId_FK_ARCHIVOS_CREDITO")]
        [Required, MaxLength(450)]
        public string UserId_FK_ARCHIVOS_CREDITO { get; set; }

        [Column("Nombre_Archivo")]
        [Required, MaxLength(128)]
        public string Nombre_Archivo { get; set; }

        [Column("Url_Archivo")]
        [Required, MaxLength(128)]
        public string Url_Archivo { get; set; }

        [Column("Fecha_Creacion")]
        public DateTime Fecha_Creacion { get; set; }

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
