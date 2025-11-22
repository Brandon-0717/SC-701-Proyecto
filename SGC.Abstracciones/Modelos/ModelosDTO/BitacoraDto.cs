using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.Modelos.ModelosDTO
{
    public class BitacoraDto
    {
        public int Id { get; set; }              // Identificador único autoincremental
        public int Gestion { get; set; }         // Número de gestión (correlativo o asignado manualmente)
        public string Accion { get; set; }       // Acción realizada (Insertar, Actualizar, Eliminar, etc.)
        public string Comentario { get; set; }   // Texto libre para observaciones
        public string Usuario { get; set; }      // Usuario que realizó la acción
        public DateTime Fecha { get; set; }


    }
}
