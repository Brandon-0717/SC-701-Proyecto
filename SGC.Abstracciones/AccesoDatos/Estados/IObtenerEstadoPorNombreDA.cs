using SGC.Abstracciones.Modelos.ModeloDA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.AccesoDatos.Estados
{
    public interface IObtenerEstadoPorNombreDA
    {
        Task<EstadoDA?> ObtenerPorNombreAsync(string nombreEstado);
    }
}
