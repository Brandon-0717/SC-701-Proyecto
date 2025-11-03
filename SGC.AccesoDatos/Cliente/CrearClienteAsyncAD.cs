using SGC.Abstracciones.AccesoDatos.Cliente;
using SGC.AccesoDatos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Cliente
{
    public class CrearClienteAsyncAD : ICrearClienteAsyncAD
    {
        private Contexto _contexto;

        public CrearClienteAsyncAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> CrearClienteAsync(ClienteDA cliente)
        {
            try
            {
                // Si el cliente no tiene un GUID, lo generamos
                if (!cliente.CLIENTES_PK.HasValue || cliente.CLIENTES_PK == Guid.Empty)
                    cliente.CLIENTES_PK = Guid.NewGuid();

                // Fecha de creación automática
                cliente.Fecha_Creacion = DateTime.Now;

                // Se agrega a la tabla CLIENTES
                await _contexto.Clientes.AddAsync(cliente);

                // Guardamos los cambios
                await _contexto.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el cliente en la base de datos: {ex.Message} -- {ex.InnerException?.Message}", ex);
            }
        }
    }
}
