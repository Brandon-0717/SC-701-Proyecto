
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class ObtenerUsuarioPorIdDA : IObtenerUsuarioPorIdDA
    {
        private readonly Contexto _context;
        public ObtenerUsuarioPorIdDA(Contexto context)
        {
            _context = context;
        }
        public async Task<UsuarioDA> Obtener(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
