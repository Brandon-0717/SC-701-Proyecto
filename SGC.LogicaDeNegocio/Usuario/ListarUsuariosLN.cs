
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class ListarUsuariosLN : IListarUsuariosLN
    {
        private readonly IListarUsuariosDA _listarUsuariosDA;
        private readonly IMapper _mapper;
        public ListarUsuariosLN(IListarUsuariosDA listarUsuariosDA, IMapper mapper)
        {
            _listarUsuariosDA = listarUsuariosDA;
            _mapper = mapper;
        }
        public async Task<CustomResponse<List<UsuarioDA>>> Obtener()
        {
            var response = new CustomResponse<List<UsuarioDA>>();

            var usuarios = await _listarUsuariosDA.Obtener();

            if (!usuarios.Any() || usuarios.Count < 0)
            {
                response.EsError = true;
                response.Mensaje = "No se encontraron usuarios.";
                return response;
            }

            response.Mensaje = "Usuarios encontrados exitosamente.";
            response.Data = _mapper.Map<List<UsuarioDA>>(usuarios);
            return response;
        }
    }
}
