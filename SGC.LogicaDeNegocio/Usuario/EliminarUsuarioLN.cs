
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class EliminarUsuarioLN : IEliminarUsuarioLN
    {
        private readonly IEliminarUsuarioDA _eliminarUsuarioDA;
        private readonly IObtenerUsuarioPorIdLN _obtenerUsuarioPorIdLN;
        private readonly IMapper _mapper;

        public EliminarUsuarioLN(IEliminarUsuarioDA eliminarUsuarioDA, IObtenerUsuarioPorIdLN obtenerUsuarioPorIdLN, IMapper mapper)
        {
            _eliminarUsuarioDA = eliminarUsuarioDA;
            _obtenerUsuarioPorIdLN = obtenerUsuarioPorIdLN;
            _mapper = mapper;
        }
        public async Task<CustomResponse<bool>> Eliminar(string id)
        {
            var response = new CustomResponse<bool>();

            var usuarioExistente = await _obtenerUsuarioPorIdLN.Obtener(id);

            if (usuarioExistente.EsError)
            {
                response.EsError = true;
                response.Mensaje = "Usuario no existe.";
                return response;
            }

            bool eliminado = await _eliminarUsuarioDA.Eliminar(id);
            
            if (!eliminado)
            {
                response.EsError = true;
                response.Mensaje = "Error al eliminar el usuario.";
            }

            response.Mensaje = "Usuario eliminado exitosamente.";
            return response;
        }
    }
}
