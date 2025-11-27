using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGC.Abstracciones.Modelos;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.Abstracciones.LogicaDeNegocio.Solicitud
{
    public interface ISolicitudCreditoLN
    {
        Task<CustomResponse<Guid>> CrearSolicitudAsync(SolicitudCreditoCrearDto dto, string userId, string creadoPor);
    }
}
