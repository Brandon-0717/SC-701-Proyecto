
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;

namespace SGC.LogicaDeNegocio.Roles
{
    public class ValidarExistenciaRolPorIdLN : IValidarExistenciaRolPorIdLN
    {
        private readonly IvalidarExistenciaRolPorIdDA _validarExistenciaRolPorIdDA;

        public ValidarExistenciaRolPorIdLN(IvalidarExistenciaRolPorIdDA validarExistenciaRolPorIdDA)
        {
            _validarExistenciaRolPorIdDA = validarExistenciaRolPorIdDA;
        }

        public async Task<CustomResponse<bool>> Validar(string idRol)
        {
            var response = new CustomResponse<bool>();

            bool existe = await _validarExistenciaRolPorIdDA.Validar(idRol);

            if (existe)
            {
                response.Mensaje = "El rol existe.";
                return response;
            }
            else
            {
                response.EsError = true;
                response.Mensaje = "El rol no existe.";
                return response;
            }
        }
    }
}
