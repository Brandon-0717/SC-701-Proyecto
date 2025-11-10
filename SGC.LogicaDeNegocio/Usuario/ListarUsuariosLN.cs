
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Estados;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class ListarUsuariosLN : IListarUsuariosLN
    {
        private readonly IListarUsuariosDA _listarUsuariosDA;
        private readonly IMapper _mapper;
        private readonly IObtenerEstadoPorIdLN _obtenerEstadoPorIdLN;
        private readonly IObtenerRolesPorIdUsuarioLN _ObtenerRolesPorIdUsuarioLN;

        public ListarUsuariosLN(IListarUsuariosDA listarUsuariosDA, IMapper mapper, IObtenerEstadoPorIdLN obtenerEstadoPorIdLN, IObtenerRolesPorIdUsuarioLN ObtenerRolesPorIdUsuarioLN)
        {
            _listarUsuariosDA = listarUsuariosDA;
            _mapper = mapper;
            _obtenerEstadoPorIdLN = obtenerEstadoPorIdLN;
            _ObtenerRolesPorIdUsuarioLN = ObtenerRolesPorIdUsuarioLN;
        }
        public async Task<CustomResponse<List<UsuarioDTO>>> Obtener()
        {
            //****NOTA IMPORTANTE****: ACTUALMENTE HAY UN PROBLEMA DE RENDIMIENTO
            //DEBIDO A LA CANTIDAD DE LLAMADAS A LA DB QUE SE REALIZAN
            //AL NOMBRAR LOS ROLES Y ESTADOS DE CADA USUARIO

            var response = new CustomResponse<List<UsuarioDTO>>();

            var usuarios = await _listarUsuariosDA.Obtener();

            if (!usuarios.Any() || usuarios.Count < 0)
            {
                response.EsError = true;
                response.Mensaje = "No se encontraron usuarios.";
                return response;
            }

            var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);
            usuariosDTO = await NombrarRol(usuariosDTO);
            usuariosDTO =  await NombrarEstado(usuariosDTO);

            response.Mensaje = "Usuarios encontrados exitosamente.";
            response.Data = usuariosDTO;
            return response;
        }

        private async Task<List<UsuarioDTO>> NombrarRol(List<UsuarioDTO> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                var customResRol= await _ObtenerRolesPorIdUsuarioLN.Obtener(usuario.Id);
                
                if (!customResRol.EsError)
                {
                    usuario.Roles = customResRol.Data;
                }
            }  
            return usuarios;
        }

        private async Task<List<UsuarioDTO>> NombrarEstado(List<UsuarioDTO> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                var customResEstado = await _obtenerEstadoPorIdLN.Obtener(usuario.Estados_FK_AspNetUsers.ToString());
                if (!customResEstado.EsError)
                {
                    usuario.NombreEstado = customResEstado.Data.Nombre_Estado;
                }
            }
            return usuarios;
        }


    }
}
