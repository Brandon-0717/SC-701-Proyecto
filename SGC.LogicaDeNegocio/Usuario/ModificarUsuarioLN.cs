
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class ModificarUsuarioLN : IModificarUsuarioLN
    {
        private readonly IModificarUsuarioDA _modificarUsuarioDA;
        private readonly IObtenerUsuarioPorIdLN _obtenerUsuarioPorIdLN;
        private readonly IObtenerUsuarioPorIdentificacionLN _obtenerUsuarioPorIdentificacionLN;
        private readonly IMapper _mapper;
        private readonly UserManager<UsuarioDA> _userManager;
        public ModificarUsuarioLN(
            IModificarUsuarioDA modificarUsuarioDA,
            IObtenerUsuarioPorIdLN obtenerUsuarioPorIdLN,
            IMapper mapper,
            IObtenerUsuarioPorIdentificacionLN obtenerUsuarioPorIdentificacionLN,
            UserManager<UsuarioDA> userManager
            )
        {
            _modificarUsuarioDA = modificarUsuarioDA;
            _obtenerUsuarioPorIdLN = obtenerUsuarioPorIdLN;
            _mapper = mapper;
            _obtenerUsuarioPorIdentificacionLN = obtenerUsuarioPorIdentificacionLN;
            _userManager = userManager;
        }

        public async Task<CustomResponse<UsuarioDTO>> ModificarUsuario(UsuarioDTO usuario)
        {
            var customResponse = new CustomResponse<UsuarioDTO>();
            
            var validarResponse = await Validar(usuario);

            if (validarResponse.EsError)
            {
                return validarResponse;
            }

            var usuarioDA = _mapper.Map<UsuarioDA>(usuario);
            usuarioDA.PhoneNumber = usuario.Telefono;

            bool modificado = await _modificarUsuarioDA.ModificarUsuario(usuarioDA);

            if (!modificado)
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "No se pudo modificar el usuario.";
                return customResponse;
            }

            bool rolesModificados = await _modificarUsuarioDA.ActualizarRoles(usuario.Identificacion,usuario.NombreRoles);

            if (!rolesModificados)
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "No se pudieron modificar los roles del usuario.";
                return customResponse;
            }

            customResponse.Mensaje = "Usuario modificado exitosamente.";
            customResponse.Data = usuario;
            return customResponse;
        }

        private async Task<CustomResponse<UsuarioDTO>> Validar(UsuarioDTO usuario)
        {
            var customResponse = new CustomResponse<UsuarioDTO>();

            //Validar existencia del usuario
            var usuarioExistenteResponse = await _obtenerUsuarioPorIdLN.Obtener(usuario.Id);
            if (usuarioExistenteResponse.EsError)
            {
                customResponse.EsError = true;
                customResponse.Mensaje = usuarioExistenteResponse.Mensaje;
                return customResponse;
            }

            //Validar que la identificacion no esté en uso por otro usuario
            var usuarioIdentificacionResponse = await _obtenerUsuarioPorIdentificacionLN.Obtener(usuario.Identificacion);
            if (!usuarioIdentificacionResponse.EsError && usuarioIdentificacionResponse.Data.Id != usuario.Id)
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "La identificación ya está en uso por otro usuario.";
                return customResponse;
            }

            //Validar que el email no esté en uso por otro usuario'
            var usuarioEmail = await _userManager.FindByEmailAsync(usuario.Email);
            if (usuarioEmail != null && usuarioEmail.Id != usuario.Id)
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "El correo electrónico ya está en uso por otro usuario.";
                return customResponse;
            }

            return customResponse;
        }
    }
}
