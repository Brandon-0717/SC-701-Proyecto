using SGC.Abstracciones.AccesoDatos.Estados;
using SGC.Abstracciones.AccesoDatos.Solicitud;
using SGC.Abstracciones.LogicaDeNegocio.Solicitud;
using SGC.Abstracciones.Modelos.ModeloDA;

namespace SGC.LogicaDeNegocio.Solicitud
{
    public class ListarSolicitudesPorRolLN : IListarSolicitudesPorRolLN
    {
        private readonly IListarSolicitudesPorEstadosDA _listarPorEstadosDA;
        private readonly IObtenerEstadoPorNombreDA _obtenerEstadoPorNombreDA;

        public ListarSolicitudesPorRolLN(
            IListarSolicitudesPorEstadosDA listarPorEstadosDA,
            IObtenerEstadoPorNombreDA obtenerEstadoPorNombreDA)
        {
            _listarPorEstadosDA = listarPorEstadosDA;
            _obtenerEstadoPorNombreDA = obtenerEstadoPorNombreDA;
        }

        public async Task<List<SolicitudCreditoDA>> ListarAsync(string rol)
        {
            // TODOS o ADMIN = todas las solicitudes
            if (rol == "Todos" || rol == "Admin")
            {
                var nombres = new List<string>
        {
            "REGISTRADO",
            "DEVOLUCION",
            "Activo",
            "Pendiente",
            "Suspendido"
        };

                var ids = new List<Guid>();

                foreach (var nombre in nombres)
                {
                    var estado = await _obtenerEstadoPorNombreDA.ObtenerPorNombreAsync(nombre);
                    if (estado != null)
                        ids.Add(estado.ESTADOS_PK);
                }

                return await _listarPorEstadosDA.ListarPorEstadosAsync(ids);
            }

            // Analista
            if (rol == "Analista")
            {
                var nombres = new List<string>
        {
            "REGISTRADO",
            "DEVOLUCION"
        };

                var ids = new List<Guid>();

                foreach (var nombre in nombres)
                {
                    var estado = await _obtenerEstadoPorNombreDA.ObtenerPorNombreAsync(nombre);
                    if (estado != null)
                        ids.Add(estado.ESTADOS_PK);
                }

                return await _listarPorEstadosDA.ListarPorEstadosAsync(ids);
            }

            // Gestor
            if (rol == "Gestor")
            {
                var nombres = new List<string>
        {
            "Pendiente"
        };

                var ids = new List<Guid>();

                foreach (var nombre in nombres)
                {
                    var estado = await _obtenerEstadoPorNombreDA.ObtenerPorNombreAsync(nombre);
                    if (estado != null)
                        ids.Add(estado.ESTADOS_PK);
                }

                return await _listarPorEstadosDA.ListarPorEstadosAsync(ids);
            }

            return new List<SolicitudCreditoDA>();
        }

    }
}
