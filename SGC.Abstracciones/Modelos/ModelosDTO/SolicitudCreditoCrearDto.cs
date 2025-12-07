using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class SolicitudCreditoCrearDto
    {
        [Required]
        public int Cedula { get; set; }

        [Required, Range(0, 10_000_000)]
        public decimal MontoCredito { get; set; }

        [Required, MaxLength(256)]
        public string Comentario { get; set; }

        [Required]
        public Guid CategoriaCreditoId { get; set; }

        public List<ArchivoCreditoDto> Archivos { get; set; } = new();
    }

    public class ArchivoCreditoDto
    {
        [Required, MaxLength(128)]
        public string NombreArchivo { get; set; }

        [Required, MaxLength(128)]
        public string UrlArchivo { get; set; }
    }
}
