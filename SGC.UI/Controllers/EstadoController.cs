using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Estados;

namespace SGC.UI.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IListarEstadosLN _listarEstadosLN;
        private readonly IObtenerEstadoPorIdLN _obtenerEstadoPorIdLN;

        public EstadoController(
            IListarEstadosLN listarEstadosLN,
            IObtenerEstadoPorIdLN obtenerEstadoPorIdLN
            )
        {
            _listarEstadosLN = listarEstadosLN;
            _obtenerEstadoPorIdLN = obtenerEstadoPorIdLN;
        }

        [HttpGet]
        public async Task<IActionResult> ListarEstados()
        {
            var response = await _listarEstadosLN.Listar();
            return Json(response);
        }


    }
}
