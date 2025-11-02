
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.Abstracciones.Modelos.ModeloDA
{
    [Table("SOLICITUDES_CREDITO")]
    public class SolicitudCreditoDA
    {
        [Key]
        [Column("SOLICITUDES_CREDITO_PK")]
        public Guid SOLICITUDES_CREDITO_PK { get; set; }

        [Column("ESTADOS_FK_SOLICITUDES_CREDITO")]
        public Guid ESTADOS_FK_SOLICITUDES_CREDITO { get; set; }

        [Column("CATEGORIAS_CREDITO_FK_SOLICITUDES_CREDITO")]

        public Guid CATEGORIAS_CREDITO_FK_SOLICITUDES_CREDITO { get; set; }

        [Column("UserId_FK_SOLICITUES_CREDITO")]
        [Required, MaxLength(450)]
        public string UserId_FK_SOLICITUES_CREDITO { get; set; }

        [Column("CLIENTES_FK_SOLICITUES_CREDITO")]
        public Guid CLIENTES_FK_SOLICITUES_CREDITO { get; set; }

        [Required]
        [Column("Monto_Credito")]
        public decimal Monto_Credito { get; set; }

        [Required]
        [MaxLength(256)]
        [Column("Comentario")]
        public string Comentario { get; set; }

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
