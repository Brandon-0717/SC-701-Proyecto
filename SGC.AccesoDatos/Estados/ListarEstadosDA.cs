
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.AccesoDatos.Estados
{
    public class ListarEstadosDA : IListarEstadosDA
    {
        private readonly Contexto _contexto;
        public ListarEstadosDA(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<EstadoDA>> Obtener()
        {
            return await _contexto.Estados.ToListAsync();
        }
    }
}
