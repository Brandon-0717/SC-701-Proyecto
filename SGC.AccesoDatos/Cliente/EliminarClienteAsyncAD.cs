using Microsoft.EntityFrameworkCore;
using SGC.Abstracciones.AccesoDatos.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Cliente
{
    public class EliminarClienteAsyncAD : IEliminarClienteAsyncAD
    {
        private Contexto _contexto;
        public EliminarClienteAsyncAD(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<bool> EliminarClienteAsync(Guid clienteId)
        {
            try
            {
                var cliente = await _contexto.Clientes.FirstOrDefaultAsync(c => c.CLIENTES_PK == clienteId);

                if (cliente == null)
                    return false;

                _contexto.Clientes.Remove(cliente);
                await _contexto.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Registrar error si se quiere
                throw new Exception($"Error al eliminar el cliente: {ex.Message}", ex);
            }
        }
    }
}