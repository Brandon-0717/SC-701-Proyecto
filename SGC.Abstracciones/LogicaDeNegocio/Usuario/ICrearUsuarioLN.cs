
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface ICrearUsuarioLN
    {
        Task<CustomResponse<UsuarioDTO>> Crear(UsuarioDTO usuarioDTO, string urlBase);

    }
}
