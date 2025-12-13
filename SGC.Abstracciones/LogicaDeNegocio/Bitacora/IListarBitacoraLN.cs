using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.LogicaDeNegocio.Bitacora
{
    public interface IListarBitacoraLN
    {

        List<BitacoraDto> Obtener();
    }
}
