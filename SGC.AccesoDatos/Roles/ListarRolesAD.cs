
using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Roles
{
    public class ListarRolesAD : IListarRolesAD
    {
        private readonly Contexto _context;

        public ListarRolesAD(Contexto context)
        {
            _context = context;
        }

        public Task<List<RolDA>> Listar()
        {
            return _context.Roles.ToListAsync();
        }
    }
}
