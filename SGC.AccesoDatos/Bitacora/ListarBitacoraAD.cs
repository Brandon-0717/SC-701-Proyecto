using SGC.Abstracciones.AccesoDatos.Bitacora;
using SGC.Abstracciones.Modelos.ModeloDA;
using SGC.Abstracciones.Modelos.ModelosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.AccesoDatos.Bitacora
{
    public class ListarBitacoraAD : IListarBitacoraAD
    {

        private Contexto _contexto;

        public ListarBitacoraAD(Contexto contexto)
        {
            _contexto = contexto;
        }


        public List<BitacoraDto> Obtener()
        {
            List<BitacoraDA> listaBitacoras = _contexto.Bitacora.ToList();
            List<BitacoraDto> listaBitacorasARetornar = (from bitacora in _contexto.Bitacora
                                                         select new BitacoraDto
                                                        {
                                                         Id = bitacora.Id,
                                                         Gestion = bitacora.Gestion,
                                                         Accion = bitacora.Accion,
                                                         Comentario = bitacora.Comentario,
                                                         Usuario = bitacora.Usuario,
                                                         Fecha = bitacora.Fecha
                                                     }).ToList();
            return listaBitacorasARetornar;
        }
    }
}
