using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class ClienteDto
    {
        [Required(ErrorMessage = "El campo CLIENTES_PK es requerido.")]
        public Guid CLIENTES_PK { get; set; }

        [Required(ErrorMessage = "El campo ESTADOS_FK_CLIENTES es requerido.")]
        public Guid ESTADOS_FK_CLIENTES { get; set; }

        [Required(ErrorMessage = "El campo Cédula es requerido.")]
        public int Cedula { get; set; }

        [Required(ErrorMessage = "El campo Primer Nombre es requerido.")]
        [MaxLength(128, ErrorMessage = "El campo Primer Nombre no puede tener más de 128 caracteres.")]
        public string Primer_Nombre { get; set; }

        [MaxLength(128, ErrorMessage = "El campo Segundo Nombre no puede tener más de 128 caracteres.")]
        public string Segundo_Nombre { get; set; }

        [Required(ErrorMessage = "El campo Primer Apellido es requerido.")]
        [MaxLength(128, ErrorMessage = "El campo Primer Apellido no puede tener más de 128 caracteres.")]
        public string Primer_Apellido { get; set; }

        [Required(ErrorMessage = "El campo Segundo Apellido es requerido.")]
        [MaxLength(128, ErrorMessage = "El campo Segundo Apellido no puede tener más de 128 caracteres.")]
        public string Segundo_Apellido { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Nacimiento es requerido.")]
        public DateTime Fecha_Nacimiento { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es requerido.")]
        [MaxLength(128, ErrorMessage = "El campo Teléfono no puede tener más de 128 caracteres.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico es requerido.")]
        [MaxLength(128, ErrorMessage = "El campo Correo Electrónico no puede tener más de 128 caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del Correo Electrónico no es válido.")]
        public string Correo_Electronico { get; set; }

        [Required(ErrorMessage = "El campo Sexo es requerido.")]
        public bool Sexo { get; set; }

        [Required(ErrorMessage = "El campo Dirección Exacta es requerido.")]
        [MaxLength(128, ErrorMessage = "El campo Dirección Exacta no puede tener más de 128 caracteres.")]
        public string Direccion_Exacta { get; set; }

        public DateTime ? Fecha_Creacion { get; set; }

        [MaxLength(128, ErrorMessage = "El campo Creado Por no puede tener más de 128 caracteres.")]
        public string ? CreadoPor { get; set; }

        [MaxLength(128, ErrorMessage = "El campo Modificado Por no puede tener más de 128 caracteres.")]
        public string ? ModificadoPor { get; set; }

        public DateTime ? Fecha_Modificacion { get; set; }
    }
}
