
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface IModificarUsuarioLN
    {
        Task<CustomResponse<UsuarioDTO>> ModificarUsuario(UsuarioDTO usuario);
    }
}
