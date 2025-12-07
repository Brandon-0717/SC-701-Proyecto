using SGC.Abstracciones.AccesoDatos.Bitacora;
using SGC.Abstracciones.LogicaDeNegocio.Bitacora;
using SGC.Abstracciones.Modelos.ModelosDTO;
using SGC.AccesoDatos.Bitacora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGC.LogicaDeNegocio.Bitacora
{
    public class CrearBitacoraLN : ICrearBitacoraLN
    {
        private ICrearBitacoraAD _crearBitacoraAD;

        public CrearBitacoraLN(ICrearBitacoraAD crearBitacoraAD)
        {
            _crearBitacoraAD = crearBitacoraAD;
        }



        public void CrearBitacora(BitacoraDto labitacora)
        {
            try { 
                labitacora.Fecha = DateTime.Now;
                _crearBitacoraAD.Guardar(labitacora);

            }catch(Exception ex){ 
                
                throw new Exception("Error al crear la bitacora: " + ex.Message);


            }
        }






    }
}
