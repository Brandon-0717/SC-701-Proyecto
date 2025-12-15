using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SGC.Abstracciones.AccesoDatos.Solicitud;
using SGC.Abstracciones.LogicaDeNegocio.Bitacora;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Solicitud
{
    public class SolicitudCreditoLN : ISolicitudCreditoLN
    {
        private readonly ISolicitudCreditoDA _da;
        private ICrearBitacoraLN _crearBitacoraLN;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SolicitudCreditoLN(ISolicitudCreditoDA da, ICrearBitacoraLN crearBitacoraLN, IHttpContextAccessor httpContextAccessor)
        {
            _da = da;
            _crearBitacoraLN = crearBitacoraLN;
            _httpContextAccessor = httpContextAccessor;
        }

  

        public async Task<CustomResponse<Guid>> CrearSolicitudAsync(
            SolicitudCreditoCrearDto dto, string userId, string creadoPor)
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;

            var resp = new CustomResponse<Guid>();

            if (dto.MontoCredito > 10_000_000)
            {
                resp.EsError = true;
                resp.Mensaje = "No se permite un monto mayor a ₡10,000,000.";
                return resp;
            }

            var clienteId = await _da.ObtenerClienteIdPorCedulaAsync(dto.Cedula);
            if (clienteId is null)
            {
                resp.EsError = true;
                resp.Mensaje = $"No existe un cliente con cédula {dto.Cedula}.";
                return resp;
            }

            var registradoId = await _da.ObtenerEstadoIdPorNombreAsync("REGISTRADO");
            var devolucionId = await _da.ObtenerEstadoIdPorNombreAsync("DEVOLUCION");

            if (registradoId is null || devolucionId is null)
            {
                resp.EsError = true;
                resp.Mensaje = "No se encontraron los estados REGISTRADO o DEVOLUCIÓN.";
                return resp;
            }

            var yaExiste = await _da.ExisteSolicitudAbiertaAsync(
                clienteId.Value, registradoId.Value, devolucionId.Value);

            if (yaExiste)
            {
                resp.EsError = true;
                resp.Mensaje = $"El usuario con identificación {dto.Cedula} ya cuenta con una solicitud en estado REGISTRADO o DEVOLUCIÓN.";
                return resp;
            }

            var id = await _da.CrearSolicitudAsync(
                clienteId.Value, registradoId.Value, dto.CategoriaCreditoId,
                userId, dto.MontoCredito, dto.Comentario, creadoPor);

            BitacoraDto bitacoraDto = new BitacoraDto
            {
                Gestion = dto.Cedula,
                Accion = "Creacion de solicitud Crédito",
                Comentario = $"Se crea  gestion para cliente: {dto.Cedula}",
                Usuario = userName,
                Fecha = DateTime.UtcNow
            };
            _crearBitacoraLN.CrearBitacora(bitacoraDto);

            /* BLOQUE PARA PEGAR DESPUES DE CAMBIAR ESTADOS
             * 
            BitacoraDto bitacoraDto = new BitacoraDto
            {
                Gestion = dto.Cedula,
                Accion = "EDIT",
                Comentario = $"Se cambia  el estado para la gestion {dto.Cedula} a {'Reemplazar por atributo estado'}",
                Usuario = userName,
                Fecha = DateTime.UtcNow
            };
            _crearBitacoraLN.CrearBitacora(bitacoraDto);

            */



            if (dto.Archivos.Any())
                await _da.AgregarArchivosAsync(id, userId, dto.Archivos, creadoPor);

            resp.Data = id;
            resp.Mensaje = "Solicitud creada correctamente.";



            return resp;
        }
    }
}
