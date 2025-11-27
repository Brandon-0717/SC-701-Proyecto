using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.AccesoDatos.Usuario
{
    public class CrearUsuarioDA : ICrearUsuarioDA
    {
        private readonly UserManager<UsuarioDA> _userManager;

        public CrearUsuarioDA(UserManager<UsuarioDA> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Crear(UsuarioDA usuario, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(usuario, password);

                if (!result.Succeeded)
                {
                    // Mostrar los errores específicos de Identity
                    var errores = string.Join(" | ", result.Errors.Select(e => e.Description));
                    Console.WriteLine($"[IdentityError] No se pudo crear el usuario: {errores}");

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                // Mostrar detalles completos de la excepción
                Console.WriteLine("[Exception] Error creando usuario:");
                Console.WriteLine($"Mensaje: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                return false;
            }
        }

    }
}
