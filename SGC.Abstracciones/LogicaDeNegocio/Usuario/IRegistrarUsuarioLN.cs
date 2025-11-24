
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface IRegistrarUsuarioLN
    {
        Task<CustomResponse<UsuarioRegistroModelDTO>> Registrar(UsuarioRegistroModelDTO usuarioDTO, string urlBase);
    }
}
