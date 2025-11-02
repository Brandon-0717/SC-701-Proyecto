
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Roles
{
    public class CrearRolDA : ICrearRolDA
    {
        private readonly Contexto _context;

        public CrearRolDA(Contexto context)
        {
            _context = context;
        }

        public async Task<bool> Crear(RolDA rol)
        {
            await _context.Roles.AddAsync(rol); //Agregar el rol al contexto
            var resultado = await _context.SaveChangesAsync() > 0; //Guardar los cambios en la base de datos
            return resultado;
        }
    }
}
