
using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Estados
{
    public class ObtenerEstadoPorIdDA : IObtenerEstadoPorIdDA
    {
        private readonly Contexto _context;
        public ObtenerEstadoPorIdDA(Contexto context)
        {
            _context = context;
        }
        public async Task<EstadoDA> Obtener(Guid id)
        {
            return await _context.Estados.FindAsync(id);
        }
    }
}
