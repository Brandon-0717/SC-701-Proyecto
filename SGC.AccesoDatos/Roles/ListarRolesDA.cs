
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Roles
{
    public class ListarRolesDA : IListarRolesDA
    {
        private readonly Contexto _context;

        public ListarRolesDA(Contexto context)
        {
            _context = context;
        }

        public Task<List<RolDA>> Listar()
        {
            return _context.Roles.ToListAsync();
        }
    }
}
