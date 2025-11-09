
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface IListarUsuariosLN
    {
        Task<CustomResponse<List<UsuarioDTO>>> Obtener();
    }
}
