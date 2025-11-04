using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.AccesoDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Cliente
{
    public class ActualizarClienteAsyncAD : IActualizarClienteAsyncAD
    {
        private Contexto _contexto;

        public ActualizarClienteAsyncAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> ActualizarClienteAsync(ClienteDA cliente)
        {
            try
            {
                // Buscar cliente existente
                var clienteExistente = await _contexto.Clientes
                    .FirstOrDefaultAsync(c => c.CLIENTES_PK == cliente.CLIENTES_PK);

                if (clienteExistente == null)
                    throw new Exception("El cliente no existe en la base de datos.");

                // Actualizar campos
                clienteExistente.ESTADOS_FK_CLIENTES = cliente.ESTADOS_FK_CLIENTES;
                clienteExistente.Cedula = cliente.Cedula;
                clienteExistente.Primer_Nombre = cliente.Primer_Nombre;
                clienteExistente.Segundo_Nombre = cliente.Segundo_Nombre;
                clienteExistente.Primer_Apellido = cliente.Primer_Apellido;
                clienteExistente.Segundo_Apellido = cliente.Segundo_Apellido;
                clienteExistente.Fecha_Nacimiento = cliente.Fecha_Nacimiento;
                clienteExistente.Telefono = cliente.Telefono;
                clienteExistente.Correo_Electronico = cliente.Correo_Electronico;
                clienteExistente.Sexo = cliente.Sexo;
                clienteExistente.Direccion_Exacta = cliente.Direccion_Exacta;
                clienteExistente.ModificadoPor = cliente.ModificadoPor;
                clienteExistente.Fecha_Modificacion = DateTime.Now;

                // Guardar cambios
                _contexto.Clientes.Update(clienteExistente);
                await _contexto.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el cliente: {ex.Message}", ex);
            }
        }
    }
}
//
