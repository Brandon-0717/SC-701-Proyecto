using Microsoft.AspNetCore.Mvc;
using SGC.Abstracciones.LogicaDeNegocio.Cliente;
using SGC.Abstracciones.Modelos.ModelosDTO;

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

        //public IActionResult Test()
        //{
        //    return Content("✅ ClienteController cargado correctamente");
        //}

        [HttpPost]
        public async Task<IActionResult> CrearCliente([FromBody] 
        ClienteDto cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _crearClienteAsyncLN.CrearClienteAsync(cliente);

            if (response.EsError)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCliente([FromBody] ClienteDto cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _actualizarClienteAsyncLN.ActualizarClienteAsync(cliente);

            if (response.EsError)
                return BadRequest(response);

            return Ok(response);
        }

    }
}

