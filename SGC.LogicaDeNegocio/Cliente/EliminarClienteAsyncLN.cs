using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.Abstracciones.LogicaDeNegocio.Cliente;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Cliente
{
    public class EliminarClienteAsyncLN : IEliminarClienteAsyncLN
    {
        private readonly IEliminarClienteAsyncAD _eliminarClienteAD;
        private readonly IMapper _mapper;

        public EliminarClienteAsyncLN(IEliminarClienteAsyncAD eliminarClienteAD, IMapper mapper)
        {
            _eliminarClienteAD = eliminarClienteAD;
            _mapper = mapper;
        }

        public async Task<CustomResponse<ClienteDto>> EliminarClienteAsync(string clienteId)
        {
            var response = new CustomResponse<ClienteDto>();

            try
            {
                if (!Guid.TryParse(clienteId, out Guid idGuid))
                {
                    response.EsError = true;
                    response.Mensaje = "El identificador del cliente no es válido.";
                    return response;
                }

                var eliminado = await _eliminarClienteAD.EliminarClienteAsync(idGuid);

                if (!eliminado)
                {
                    response.EsError = true;
                    response.Mensaje = "No se pudo eliminar el cliente o no se encontró.";
                    return response;
                }

                response.EsError = false;
                response.Mensaje = "Cliente eliminado correctamente.";
                response.Data = null; // No devolvemos datos del cliente
            }
            catch (Exception ex)
            {
                response.EsError = true;
                response.Mensaje = $"Error al eliminar el cliente: {ex.Message}";
            }

            return response;
        }
    }
}