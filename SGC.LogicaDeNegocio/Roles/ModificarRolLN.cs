
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class ModificarRolLN : IModificarRolLN
    {
        private readonly IModificarRolDA _modificarRolDA;
        private readonly IValidarExistenciaRolPorIdLN _validarExistenciaRolPorIdLN;
        private readonly IMapper _mapper;

        public ModificarRolLN(IModificarRolDA modificarRolDA, IMapper mapper, IValidarExistenciaRolPorIdLN validarExistenciaRolPorIdLN)
        {
            _modificarRolDA = modificarRolDA;
            _mapper = mapper;
            _validarExistenciaRolPorIdLN = validarExistenciaRolPorIdLN;
        }

        public async Task<CustomResponse<RolDTO>> Modificar(RolDTO rol)
        {
            var response = new CustomResponse<RolDTO>();

            if(rol == null || string.IsNullOrEmpty(rol.Id) || string.IsNullOrEmpty(rol.Name))
            {
                response.EsError = true;
                response.Mensaje = "No se puede recibir un rol NULO.";
                return response;
            }

            var existe = await _validarExistenciaRolPorIdLN.Validar(rol.Id);

            if (existe.EsError)
            {
                response.EsError = true;
                response.Mensaje = "El rol que se desea modificar NO existe.";
                return response;
            }

            //Limpieza del objeto
            var rolDA = _mapper.Map<RolDA>(rol);
            rolDA.Name = char.ToUpper(rol.Name[0]) + rol.Name.Substring(1).ToLower(); // Capitalizar el nombre del rol
            rolDA.NormalizedName = rol.Name.ToUpper();

            //Modificar el rol
            bool modificado = await _modificarRolDA.Modificar(rolDA);

            if (modificado)
            {
                response.Mensaje = "Rol modificado exitosamente.";
                response.Data = _mapper.Map<RolDTO>(rolDA);
                return response;
            }
            else
            {
                response.EsError = true;
                response.Mensaje = "No se pudo modificar el rol.";
                return response;
            }
        }
    }
}
