using System;
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
    public class CrearUsuarioLN : ICrearUsuarioLN
    {
        private readonly ICrearUsuarioDA _crearUsuarioDA;
        private readonly IMapper _mapper;
        private readonly IObtenerUsuarioPorIdentificacionLN _obtenerUsuarioPorIdentificacionLN;
        private readonly UserManager<UsuarioDA> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IAsignarRolesDA _asignarRolesDA;

        public CrearUsuarioLN(
            ICrearUsuarioDA crearUsuarioDA,
            IMapper mapper,
            IObtenerUsuarioPorIdentificacionLN obtenerUsuarioPorIdentificacionLN,
            UserManager<UsuarioDA> userManager,
            IEmailSender emailSender,
            IAsignarRolesDA asignarRolesDA)
        {
            _crearUsuarioDA = crearUsuarioDA;
            _mapper = mapper;
            _obtenerUsuarioPorIdentificacionLN = obtenerUsuarioPorIdentificacionLN;
            _userManager = userManager;
            _emailSender = emailSender;
            _asignarRolesDA = asignarRolesDA;
        }

        public async Task<CustomResponse<UsuarioDTO>> Crear(UsuarioDTO usuarioDTO, string urlBase)
        {
            var response = new CustomResponse<UsuarioDTO>();

            // #1 - Validaciones para evitar usuarios duplicados
            var emailExiste = await _userManager.FindByEmailAsync(usuarioDTO.Email);
            if (emailExiste != null)
            {
                response.EsError = true;
                response.Mensaje = "El correo ya está en uso.";
                return response;
            }

            var identificacionExiste = await _obtenerUsuarioPorIdentificacionLN.Obtener(usuarioDTO.Identificacion);
            if (!identificacionExiste.EsError)
            {
                response.EsError = true;
                response.Mensaje = "La identificación ya está en uso.";
                return response;
            }

            if (usuarioDTO.NombreRoles == null || !usuarioDTO.NombreRoles.Any())
            {
                response.EsError = true;
                response.Mensaje = "Debe asignar al menos un rol al usuario.";
                return response;
            }

            // #2 - Mapear el UsuarioDTO a UsuarioDA
            var usuarioDA = _mapper.Map<UsuarioDA>(usuarioDTO);
            usuarioDA.UserName = usuarioDTO.Email;
            usuarioDA.PhoneNumber = usuarioDTO.Telefono;
            usuarioDA.Id = Guid.NewGuid().ToString();

            // #3 - Generar una contraseña random
            string passwordRandom = GenerarContraseniaRamdom();

            // #4 - Crear el usuario
            var resCrearUsuario = await _crearUsuarioDA.Crear(usuarioDA,passwordRandom);

            if (!resCrearUsuario)
            {
                response.EsError = true;
                response.Mensaje = "Error al crear el usuario:";           
                return response;
            }

            // Asignar roles seleccionados
            //var resAsignarRoles = await _crearUsuarioDA.

            var resAsignarRoles = false;

            
            var rolesSeparados = usuarioDTO.NombreRoles
            .SelectMany(r => r.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Select(r => r.Trim())
            .ToList();

            resAsignarRoles = await _asignarRolesDA.AsignarRoles(usuarioDA.Identificacion, rolesSeparados);
            

            // #5 - Enviar correo de confirmación
            await EnviarCorreoConfirmacion(usuarioDA, passwordRandom, urlBase);

            if (!resAsignarRoles)
            {
                response.Mensaje = "Usuario creado pero no se asigno rol.";
                return response;
            }

            response.Mensaje = "Usuario creado con éxito.";
            return response;
        }

        private string GenerarContraseniaRamdom()
        {
            // Esta contraseña cumple los requisitos del regex del usuario DTO
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string numeros = "0123456789";
            const string todos = mayusculas + minusculas + numeros;

            var random = new Random();

            // Aseguramos al menos una de cada categoría
            var contraseña = new List<char>
            {
                mayusculas[random.Next(mayusculas.Length)],
                minusculas[random.Next(minusculas.Length)],
                numeros[random.Next(numeros.Length)]
            };

            // Rellenamos hasta alcanzar entre 8 y 12 caracteres
            int longitud = random.Next(8, 13); // 13 porque el límite superior es exclusivo
            while (contraseña.Count < longitud)
            {
                contraseña.Add(todos[random.Next(todos.Length)]);
            }

            // Mezclamos los caracteres para que no estén siempre en el mismo orden
            return new string(contraseña.OrderBy(_ => random.Next()).ToArray());
        }

        private async Task EnviarCorreoConfirmacion(UsuarioDA usuario, string password, string baseUrl)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(usuario);
            var encoded = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var link = $"{baseUrl}/Usuario/ConfirmarEmail?userId={usuario.Id}&code={encoded}";
            var safeLink = HtmlEncoder.Default.Encode(link);

            var cuerpoHtml = $@"
            <html>
            <body style='font-family: Arial, sans-serif; background-color: #f4f6f8; padding: 40px; text-align: center;'>
                <div style='background-color: #ffffff; max-width: 600px; margin: auto; border-radius: 8px; 
                            box-shadow: 0 4px 10px rgba(0,0,0,0.1); padding: 30px;'>
                    <h2 style='color: #333333;'>¡Bienvenido a <span style=""color:#007bff;"">SGC</span>!</h2>
                    <p style='color: #555555; font-size: 16px;'>
                        Tu contraseña temporal es: <strong>{password}</strong>
                    </p>
                    <p style='color: #555555; font-size: 16px;'>
                        Gracias por registrarte, <strong>{usuario.Nombre}</strong>.<br>
                        Para activar tu cuenta, confirma tu correo electrónico haciendo clic en el siguiente botón:
                    </p>
                    <a href='{safeLink}' style='display: inline-block; margin-top: 20px; background-color: #007bff; 
                                               color: #ffffff; text-decoration: none; padding: 12px 24px; 
                                               border-radius: 5px; font-weight: bold;'>
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
