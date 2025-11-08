
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.Abstracciones.LogicaDeNegocio.Usuario
{
    public interface IListarUsuariosLN
    {
        Task<CustomResponse<List<UsuarioDA>>> Obtener();
    }
}
