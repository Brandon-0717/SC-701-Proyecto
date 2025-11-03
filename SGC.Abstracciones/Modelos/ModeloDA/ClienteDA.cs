
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC.AccesoDatos.Modelos
{
    [Table("CLIENTES")]
    public class ClienteDA //NOTA: REVISAR LAS PROPIEDADES DE NAVEGACION, PUEDEN SER UTILES
    {
        [Key]
        [Column("CLIENTES_PK")]
        public Guid ? CLIENTES_PK { get; set; }

        [Column("ESTADOS_FK_CLIENTES")]
        public Guid ? ESTADOS_FK_CLIENTES { get; set; }

        [Required]
        [Column("Cedula")]
        public int ? Cedula { get; set; }

        [Required, MaxLength(128)]
        [Column("Primer_Nombre")]
        public string ?  Primer_Nombre { get; set; }

        [MaxLength(128)]
        [Column("Segundo_Nombre")]
        public string ? Segundo_Nombre { get; set; }

        [Required, MaxLength(128)]
        [Column("Primer_Apellido")]
        public string ? Primer_Apellido { get; set; }

        [MaxLength(128)]
        [Column("Segundo_Apellido")]
        public string ? Segundo_Apellido { get; set; }

        [Required]
        [Column("Fecha_Nacimiento")]
        public DateTime ? Fecha_Nacimiento { get; set; }

        [Required, MaxLength(128)]
        [Column("Telefono")]
        public string ? Telefono { get; set; }

        [Required, MaxLength(128)]
        [Column("Correo_Electronico")]
        public string ? Correo_Electronico { get; set; }

        [Required]
        [Column("Sexo")]
        public bool ? Sexo { get; set; } // BIT en SQL Server

        [Required, MaxLength(128)]
        [Column("Direccion_Exacta")]
        public string ? Direccion_Exacta { get; set; }

        [Required]
        [Column("Fecha_Creacion")]
        public DateTime ? Fecha_Creacion { get; set; }

        // Auditoría
        [MaxLength(128)]
        [Column("CreadoPor")]
        public string ? CreadoPor { get; set; }

        [MaxLength(128)]
        [Column("ModificadoPor")]
        public string ? ModificadoPor { get; set; }

        [Column("Fecha_Modificacion")]
        public DateTime? Fecha_Modificacion { get; set; }
    }
}
//
