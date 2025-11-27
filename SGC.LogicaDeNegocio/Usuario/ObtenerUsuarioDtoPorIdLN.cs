
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class ObtenerUsuarioDtoPorIdLN : IObtenerUsuarioDtoPorIdLN
    {
        private readonly IObtenerUsuarioDtoPorIdDA _obtenerUsuarioDtoPorIdDA;
        public ObtenerUsuarioDtoPorIdLN(IObtenerUsuarioDtoPorIdDA obtenerUsuarioDtoPorIdDA)
        {
            _obtenerUsuarioDtoPorIdDA = obtenerUsuarioDtoPorIdDA;
        }
        public async Task<CustomResponse<UsuarioDTO>> Obtener(string id)
        {
            var customResponse = new CustomResponse<UsuarioDTO>();

            var resultadoDA = await _obtenerUsuarioDtoPorIdDA.Obtener(id);

            if (resultadoDA == null)
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "Usuario no encontrado.";
                return customResponse;
            }

            customResponse.Mensaje = "Usuario obtenido exitosamente.";
            customResponse.Data = resultadoDA;
            return customResponse;
        }
    }
}
