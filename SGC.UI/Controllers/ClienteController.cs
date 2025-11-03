using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Cliente;

namespace SGC.UI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IObtenerClienteAsyncLN _obtenerClienteAsyncLN;
        private readonly IObtenerClientePorIdAsyncLN _obtenerClientePorIdAsyncLN;
        private readonly ICrearClienteAsyncLN _crearClienteAsyncLN;
        private readonly IActualizarClienteAsyncLN _actualizarClienteAsyncLN;
        private readonly IEliminarClienteAsyncLN _eliminarClienteAsyncLN;

        public ClienteController(
            IObtenerClienteAsyncLN obtenerClienteAsyncLN,
            IObtenerClientePorIdAsyncLN obtenerClientePorIdAsyncLN,
            ICrearClienteAsyncLN crearClienteAsyncLN,
            IActualizarClienteAsyncLN actualizarClienteAsyncLN,
            IEliminarClienteAsyncLN eliminarClienteAsyncLN)
        {

            _obtenerClienteAsyncLN = obtenerClienteAsyncLN;
            _obtenerClientePorIdAsyncLN = obtenerClientePorIdAsyncLN;
            _crearClienteAsyncLN = crearClienteAsyncLN;
            _actualizarClienteAsyncLN = actualizarClienteAsyncLN;
            _eliminarClienteAsyncLN = eliminarClienteAsyncLN;
        }


        // GET: ClienteController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            var response = await _obtenerClienteAsyncLN.ObtenerClientesAsync();
            return Ok(response);


        }

        [HttpGet]
        public async Task<IActionResult> ObtenerClientePorId(string clienteId)
        {
            var response = await _obtenerClientePorIdAsyncLN.ObtenerClientePorIdAsync(clienteId);
            if (response.EsError)

                return NotFound(response);

            return Ok(response);
        }

        public IActionResult Test()
        {
            return Content("✅ ClienteController cargado correctamente");
        }
    }
}

