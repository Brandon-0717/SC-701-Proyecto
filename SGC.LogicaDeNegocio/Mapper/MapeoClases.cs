using AutoMapper;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Mapper
{
    public class MapeoClases : Profile
    {
        public MapeoClases()
        {
            CreateMap<UsuarioDA, UsuarioDTO>();
            CreateMap<UsuarioDTO, UsuarioDA>();
            CreateMap<RolDA, RolDTO>();
            CreateMap<RolDTO, RolDA>();
        }   
    }
}
