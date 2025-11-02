
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.Abstracciones.Modelos.ModeloDA
{
    [Table("GESTIONES_CREDITO")]
    public class GestionesCreditoDA
    {
        [Key]
        [Column("GESTIONES_CREDITO_PK")]
        public Guid GESTIONES_CREDITO_PK { get; set; }

        [Column("UserId_FK_GESTIONES_CREDITO")]
        [MaxLength(450)]
        public string UserId_FK_GESTIONES_CREDITO { get; set; }

        [Column("ESTADOS_FK_GESTIONES_CREDITO")]
        public Guid ESTADOS_FK_GESTIONES_CREDITO { get; set; }  

        [Column("SOLICITUDES_CREDITO_FK_GESTIONES_CREDITO")]
        public Guid SOLICITUDES_CREDITO_FK_GESTIONES_CREDITO { get; set; }

        [Column("Comentario")]
        [MaxLength(128)]
        public string Comentario { get; set; }

        [Column("FECHA_CREACION")]
        public DateTime FECHA_CREACION { get; set; }

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
