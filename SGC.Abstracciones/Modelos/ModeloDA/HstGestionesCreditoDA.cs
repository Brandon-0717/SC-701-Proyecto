
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.Abstracciones.Modelos.ModeloDA
{
    [Table("HST_GESTIONES_CREDITO")]
    public class HstGestionesCreditoDA
    {
        [Key]
        [Column("HST_GESTIONES_CREDITO_PK")]
        public Guid HST_GESTIONES_CREDITO_PK { get; set; }

        [Column("GESTIONES_CREDITO_FK_HST_GESTIONES_CREDITO")]
        public Guid GESTIONES_CREDITO_FK_HST_GESTIONES_CREDITO { get; set; }

        [Column("UserId_FK_HST_GESTIONES_CREDITO")]
        [MaxLength(450)]
        public string UserId_FK_HST_GESTIONES_CREDITO { get; set; }

        [Column("ESTADO_ANTERIOR_FK")]
        public Guid ESTADO_ANTERIOR_FK { get; set; }

        [Column("ESTADO_NUEVO_FK")]
        public Guid ESTADO_NUEVO_FK { get; set; }

        [Column("Comentario")]
        [MaxLength(256)]
        public string Comentario { get; set; }

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
