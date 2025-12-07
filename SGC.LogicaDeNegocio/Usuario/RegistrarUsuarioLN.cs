
using System.Text;
using System.Text.Encodings.Web;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using SGC.Abstracciones.AccesoDatos.Usuario;
using SGC.Abstracciones.LogicaDeNegocio.Usuario;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.LogicaDeNegocio.Usuario
{
    public class RegistrarUsuarioLN : IRegistrarUsuarioLN
    {
        private readonly ICrearUsuarioDA _crearUsuarioDA;
        private readonly IObtenerUsuarioPorIdentificacionLN _obtenerUsuarioPorIdentificacionLN;
        private readonly UserManager<UsuarioDA> _userManager;
        private readonly IEmailSender _emailSender;

        public RegistrarUsuarioLN(
            ICrearUsuarioDA crearUsuarioDA,
            IObtenerUsuarioPorIdentificacionLN obtenerUsuarioPorIdentificacionLN,
            UserManager<UsuarioDA> userManager,
            IEmailSender emailSender
            )
        {
            _crearUsuarioDA = crearUsuarioDA;
            _obtenerUsuarioPorIdentificacionLN = obtenerUsuarioPorIdentificacionLN;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<CustomResponse<UsuarioRegistroModelDTO>> Registrar(UsuarioRegistroModelDTO usuarioRegistro, string urlBase)
        {
            var customResponse = new CustomResponse<UsuarioRegistroModelDTO>();

            // #1 - Validaciones
            var validationResponse = await Validar(usuarioRegistro);

            if (validationResponse.EsError)
            {
                return validationResponse;
            }

            // #2 - Crear el usuario

            var usuario = new UsuarioDA
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                Nombre = usuarioRegistro.Nombre,
                Identificacion = usuarioRegistro.Identificacion,
            };

            bool creado = await _crearUsuarioDA.Crear(usuario, usuarioRegistro.Contrasenia);
            //Nota: El usuario se crea sin rol, el administrador debe asignar el rol posteriormente.

            if (!creado)
            {
                customResponse.EsError = true;
                customResponse.Mensaje = "Error al crear el usuario.";
                return customResponse;
            }

            // #3 - Enviar correo de confirmación
            await EnviarCorreoConfirmacion(usuario, urlBase);
            customResponse.Data = usuarioRegistro;
            customResponse.Mensaje = "Usuario creado exitosamente. Por favor, verifica tu correo para confirmar tu cuenta.";
            return customResponse;
        }


        private async Task<CustomResponse<UsuarioRegistroModelDTO>> Validar(UsuarioRegistroModelDTO usuario)
        {
            var response = new CustomResponse<UsuarioRegistroModelDTO>();

            // #1 - Validaciones nulos
            if (usuario == null)
            {
                response.EsError = true;
                response.Mensaje = "El usuario no puede ser nulo.";
                return response;
            }

            // #2 - Validaciones para evitar usuarios duplicados
            var emailExiste = await _userManager.FindByEmailAsync(usuario.Email);
            if (emailExiste != null)
            {
                response.EsError = true;
                response.Mensaje = "El correo ya está en uso.";  
            }

            // #3 - Validar que la identificacion no esté en uso por otro usuario
            var usuarioIdentificacionResponse = await _obtenerUsuarioPorIdentificacionLN.Obtener(usuario.Identificacion);
            if (!usuarioIdentificacionResponse.EsError && usuarioIdentificacionResponse.Data.Id != usuario.Identificacion)
            {
                response.EsError = true;
                response.Mensaje = "La identificación ya está en uso por otro usuario.";
                return response;
            }

            return response;
        }

        private async Task EnviarCorreoConfirmacion(UsuarioDA usuario, string baseUrl)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var link = $"{baseUrl}/Usuario/ConfirmarEmail?userId={usuario.Id}&code={encoded}";
            var safeLink = HtmlEncoder.Default.Encode(link);

            var cuerpoHtml = $@"
            <html>
                <body style='font-family: Arial, sans-serif; background-color: #f4f6f8; padding: 40px; text-align: center;'>
                    <div style='background-color: #ffffff; max-width: 600px; margin: auto; border-radius: 8px; box-shadow: 0 4px 10px rgba(0,0,0,0.1); padding: 30px;'>
                        <h2 style='color: #333333;'>¡Bienvenido a <span style=""color:#007bff;"">SGC</span>!</h2>
                        <p style='color: #555555; font-size: 16px;'>
                            Gracias por registrarte, <strong>{usuario.Nombre}</strong>.<br>
                            Para activar tu cuenta, confirma tu correo electrónico haciendo clic en el siguiente botón:
                        </p>

                        <a href='{safeLink}'
                           style='display: inline-block; margin-top: 20px; background-color: #007bff; color: #ffffff; 
                                  text-decoration: none; padding: 12px 24px; border-radius: 5px; font-weight: bold;'>
                            Confirmar mi cuenta
                        </a>

                        <hr style='margin-top: 30px; border: none; border-top: 1px solid #eeeeee;'>
                        <p style='color: #999999; font-size: 12px;'>
                            © {DateTime.Now.Year} SGC. Todos los derechos reservados.
                        </p>
                    </div>
                </body>
                </html>";

            try
            {
                await _emailSender.SendEmailAsync(usuario.Email, "Confirma tu cuenta en SGC", cuerpoHtml);
            }
            catch (Exception ex)
            {
                // Loggear error
                throw new InvalidOperationException("No se pudo enviar el correo de confirmación.", ex);
            }
        }
    }
}
