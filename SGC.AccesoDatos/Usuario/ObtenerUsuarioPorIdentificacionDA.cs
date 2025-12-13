
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class ObtenerUsuarioPorIdentificacionDA : IObtenerUsuarioPorIdentificacionDA
    {
        private readonly Contexto _context;
        public ObtenerUsuarioPorIdentificacionDA(Contexto context)
        {
            _context = context;
        }
        public async Task<UsuarioDA> Obtener(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Identificacion == id);
        }
    }
}
