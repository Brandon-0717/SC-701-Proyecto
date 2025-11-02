
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class CrearRolLN : ICrearRolLN
    {
        private readonly ICrearRolDA _crearRolDA;
        private readonly IObtenerRolPorNombreLN _obtenerRolPorNombreLN;
        private readonly IMapper _mapper;

        public CrearRolLN(ICrearRolDA crearRolDA, IObtenerRolPorNombreLN obtenerRolPorNombreLN, IMapper mapper)
        {
            _crearRolDA = crearRolDA;
            _obtenerRolPorNombreLN = obtenerRolPorNombreLN;
            _mapper = mapper;
        }

        public async Task<CustomResponse<bool>> Crear(RolDTO rol)
        {
            var response = new CustomResponse<bool>();

            if (string.IsNullOrWhiteSpace(rol.Name))
            {
                response.EsError = true;
                response.Mensaje = "El nombre del rol no puede estar vacío.";
                return response;
            }

            // Verificar si el rol ya existe
            var respuesta = await _obtenerRolPorNombreLN.Obtener(rol.Name.ToUpper());//Upper para coincidir con el NormalizedName

            if (!respuesta.EsError)//Es decir, si devuelve un rol es que ya existe
            {
                response.EsError = true;          
                response.Mensaje = "El rol ya existe.";
                return response;
            }

            // Mapear el RolDTO a RolDA

            var rolDA = _mapper.Map<RolDA>(rol);
            rol.Id = Guid.NewGuid().ToString();
            rol.Name = char.ToUpper(rol.Name[0]) + rol.Name.Substring(1).ToLower(); // Capitalizar el nombre del rol
            rolDA.NormalizedName = rol.Name.ToUpper();

            // Crear el rol
            var agregado = await _crearRolDA.Crear(rolDA);

            if (!agregado)
            {
                response.EsError = true;
                response.Mensaje = "Error al crear el rol.";
            }
            else
            {
                response.Mensaje = "Rol creado con éxito.";
            }

            return response;
        }
    }
}
