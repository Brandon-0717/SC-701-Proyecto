using System.ComponentModel.DataAnnotations;

namespace SGC.UI.Models
{
    public class SolicitudCreditoCreateVM
    {
        [Required]
        public int Cedula { get; set; }

        [Required, Range(0, 10_000_000)]
        public decimal MontoCredito { get; set; }

        [Required, MaxLength(256)]
        public string Comentario { get; set; }

        [Required]
        public Guid CategoriaCreditoId { get; set; }

        public List<IFormFile> Archivos { get; set; } = new();
    }
}
