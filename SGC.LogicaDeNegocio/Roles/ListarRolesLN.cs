
using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Roles;
using SGC.Abstracciones.LogicaDeNegocio.Roles;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Roles
{
    public class ListarRolesLN : IListarRolesLN
    {
        private readonly IListarRolesDA _listarRolesDA;
        private readonly IMapper _mapper;

        public ListarRolesLN(IListarRolesDA listarRolesDA, IMapper mapper)
        {
            _listarRolesDA = listarRolesDA;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<RolDTO>>> Listar()
        {
            var response = new CustomResponse<List<RolDTO>>();

            var listaRolesDA = await _listarRolesDA.Listar();

            if (listaRolesDA.Count == 0)
            {
                response.EsError = true;
                response.Mensaje = "No hay roles en la tabla";
                return response;
            }

            response.Data = _mapper.Map<List<RolDTO>>(listaRolesDA);
            response.Mensaje = "Roles obtenidos con éxito";
            return response;
        }
    }
}
