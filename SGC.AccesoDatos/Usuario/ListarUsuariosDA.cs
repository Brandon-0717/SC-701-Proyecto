
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class ListarUsuariosDA : IListarUsuariosDA
    {
        private readonly Contexto _contexto;
        public ListarUsuariosDA(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<UsuarioDA>> Obtener()
        {
            return await _contexto.Users.ToListAsync();
        }
    }
}
