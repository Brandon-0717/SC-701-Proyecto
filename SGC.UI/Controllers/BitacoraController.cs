using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Bitacora;
using SGC.Abstracciones.Modelos.ModelosDTO;

namespace SGC.UI.Controllers
{
    public class BitacoraController : Controller
    {
        private IListarBitacoraLN _listarBitacoraLN;

        public BitacoraController(IListarBitacoraLN listarBitacoraLN)
        {
            _listarBitacoraLN = listarBitacoraLN;
        }




        public ActionResult Bitacora() {
            /*
             * DATOS DE PRUEBA
             * 
            var listaBitacoras = new List<BitacoraDto> {
                new BitacoraDto { Id = 1, Gestion = 1001, Accion = "Se crea la gestión para el cliente", Comentario = "Creación de registro", Usuario = "admin", Fecha = DateTime.Now },
                new BitacoraDto { Id = 2, Gestion = 1002, Accion = "Actualizar", Comentario = "Modificación de registro", Usuario = "user1", Fecha = DateTime.Now },
                new BitacoraDto { Id = 3, Gestion = 1003, Accion = "Se elimina la gestion", Comentario = "Eliminación de registro", Usuario = "user2", Fecha = DateTime.Now }


            };
            return View(listaBitacoras);*/


            //RECUPERAR DATOS DE LA BASE DE DATOS
            List<BitacoraDto> listaBitacoras =  _listarBitacoraLN.Obtener();
            return View(listaBitacoras);

        }

        


    }
}
