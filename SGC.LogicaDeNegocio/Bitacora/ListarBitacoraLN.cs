using SGC.Abstracciones.AccesoDatos.Bitacora;
using SGC.Abstracciones.LogicaDeNegocio.Bitacora;
using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Bitacora
{
    public class ListarBitacoraLN : IListarBitacoraLN
    {
        private IListarBitacoraAD _listarBitacoraAD;

        public ListarBitacoraLN(IListarBitacoraAD listarBitacoraAD)
        {
            _listarBitacoraAD = listarBitacoraAD;
        }




        public List<BitacoraDto> Obtener()
        {
            List<BitacoraDto> listaBitacora = _listarBitacoraAD.Obtener();
            return listaBitacora;
        }
    }
}
