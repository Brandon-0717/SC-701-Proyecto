using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.Abstracciones.AccesoDatos.Bitacora
{
    public interface ICrearBitacoraAD
    {
        Task<int> Guardar(BitacoraDto bitacoraAGuardar);

    }
}
