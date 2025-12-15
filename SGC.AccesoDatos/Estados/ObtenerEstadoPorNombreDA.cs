using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.Modelos.ModeloDA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Estados
{
    public class ObtenerEstadoPorNombreDA : IObtenerEstadoPorNombreDA
    {
        private readonly Contexto _contexto;

        public ObtenerEstadoPorNombreDA(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<EstadoDA?> ObtenerPorNombreAsync(string nombreEstado)
        {
            var nombreNormalizado = nombreEstado.Trim().ToUpper();

            return await _contexto.Estados
                .FirstOrDefaultAsync(e =>
                    e.Nombre_Estado.Trim().ToUpper() == nombreNormalizado
                );
        }

    }
}