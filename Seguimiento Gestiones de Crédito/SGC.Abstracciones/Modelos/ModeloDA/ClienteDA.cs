
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.AccesoDatos.Modelos
{
    [Table("CLIENTES")]
    public class ClienteDA
    {
        [Key]
        public Guid CLIENTES_PK { get; set; }

        [ForeignKey("Estado")]
        public Guid ESTADOS_FK_CLIENTES { get; set; }

        [Required]
        public int Cedula { get; set; }

        [Required, MaxLength(128)]
        public string Primer_Nombre { get; set; }

        [MaxLength(128)]
        public string Segundo_Nombre { get; set; }

        [Required, MaxLength(128)]
        public string Primer_Apellido { get; set; }

        [MaxLength(128)]
        public string Segundo_Apellido { get; set; }

        [Required]
        public DateTime Fecha_Nacimiento { get; set; }

        [Required, MaxLength(128)]
        public string Telefono { get; set; }

        [Required, MaxLength(128)]
        public string Correo_Electronico { get; set; }

        [Required]
        public bool Sexo { get; set; } // BIT en SQL Server

        [Required, MaxLength(128)]
        public string Direccion_Exacta { get; set; }

        [Required]
        public DateTime Fecha_Creacion { get; set; }

        // Auditoría
        [MaxLength(128)]
        public string CreadoPor { get; set; }

        [MaxLength(128)]
        public string ModificadoPor { get; set; }

        public DateTime? Fecha_Modificacion { get; set; }
    }
}
