using SGC.Abstracciones.Modelos.ModeloDA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.LogicaDeNegocio.Solicitud
{
    public interface IListarSolicitudesPorRolLN
    {
        Task<List<SolicitudCreditoDA>> ListarAsync(string rol);
    }
}