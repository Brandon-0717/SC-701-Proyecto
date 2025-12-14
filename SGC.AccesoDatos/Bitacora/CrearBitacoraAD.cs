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
    public class CrearBitacoraAD : ICrearBitacoraAD
    {

        private Contexto _contexto;

        public CrearBitacoraAD(Contexto contexto)
        {
            _contexto = contexto;

        }

        public async Task<int> Guardar(BitacoraDto bitacoraAGuardar)
        {

            _contexto.Bitacora.Add(convertirBitacora(bitacoraAGuardar));
            int bitacoraGuardada = await _contexto.SaveChangesAsync();
            return bitacoraGuardada;


        }



        private BitacoraDA convertirBitacora(BitacoraDto bitacoraDto)
        {
            return new BitacoraDA
            {
                Id = bitacoraDto.Id,
                Gestion = bitacoraDto.Gestion,
                Accion = bitacoraDto.Accion,
                Comentario = bitacoraDto.Comentario,
                Usuario = bitacoraDto.Usuario,
                Fecha = bitacoraDto.Fecha
            };


        }

    }
}
