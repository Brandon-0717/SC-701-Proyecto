using AutoMapper;
using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.Abstracciones.LogicaDeNegocio.Cliente;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;
using SGC.AccesoDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Cliente
{
    public class CrearClienteAsyncLN : ICrearClienteAsyncLN
    {
        private readonly ICrearClienteAsyncAD _crearClienteAsyncAD;
        private readonly IMapper _mapper;

        public CrearClienteAsyncLN(ICrearClienteAsyncAD crearClienteAsyncAD, IMapper mapper)
        {
            _crearClienteAsyncAD = crearClienteAsyncAD;
            _mapper = mapper;
        }

        public async Task<CustomResponse<ClienteDto>> CrearClienteAsync(ClienteDto clienteDto)
        {
            var response = new CustomResponse<ClienteDto>();

            try
            {
                // Validaciones básicas de datos obligatorios
                if (string.IsNullOrWhiteSpace(clienteDto.Primer_Nombre))
                    throw new ArgumentException("El primer nombre es obligatorio.");
                if (string.IsNullOrWhiteSpace(clienteDto.Primer_Apellido))
                    throw new ArgumentException("El primer apellido es obligatorio.");
                if (string.IsNullOrWhiteSpace(clienteDto.Correo_Electronico))
                    throw new ArgumentException("El correo electrónico es obligatorio.");

                // Mapear DTO a entidad de acceso a datos
                var clienteDA = _mapper.Map<ClienteDA>(clienteDto);

                // Llamar al método de acceso a datos
                var resultado = await _crearClienteAsyncAD.CrearClienteAsync(clienteDA);

                if (resultado)
                {
                    response.EsError = false;
                    response.Mensaje = "Cliente creado correctamente.";
                    response.Data = clienteDto;
                }
                else
                {
                    response.EsError = true;
                    response.Mensaje = "No se pudo crear el cliente.";
                }
            }
            catch (Exception ex)
            {
                response.EsError = true;
                response.Mensaje = $"Error al crear el cliente: {ex.Message}";
            }

            return response;
        }
    }
}
