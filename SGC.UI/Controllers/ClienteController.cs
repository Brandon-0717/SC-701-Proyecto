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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _obtenerClienteAsyncLN.ObtenerClientesAsync();

            if (response == null || response.EsError || response.Data == null)
            {
                // SIEMPRE enviar una colección, nunca null
                return View(new List<ClienteDto>());
            }

            return View(response.Data); // 👈 AQUÍ ESTÁ LA CLAVE
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

        [HttpGet]
        public IActionResult Crear()
        {
            return View("CrearCliente");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCliente(ClienteDto cliente)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        Campo = x.Key,
                        Errores = x.Value.Errors.Select(e => e.ErrorMessage)
                    })
                    .ToList();

                throw new Exception(
                    System.Text.Json.JsonSerializer.Serialize(errores)
                );
            }

            var response = await _crearClienteAsyncLN.CrearClienteAsync(cliente);

            if (response.EsError)
                throw new Exception(response.Mensaje);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var response = await _obtenerClientePorIdAsyncLN.ObtenerClientePorIdAsync(id.ToString());

            if (response.EsError)
                return NotFound();

            return View("ActualizarCliente", response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarCliente(ClienteDto cliente)
        {
            if (!ModelState.IsValid)
                return View("ActualizarCliente", cliente);

            var response = await _actualizarClienteAsyncLN.ActualizarClienteAsync(cliente);

            if (response.EsError)
            {
                ModelState.AddModelError("", response.Mensaje);
                return View("ActualizarCliente", cliente);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EliminarClientee(Guid id)
        {
            var response = await _obtenerClientePorIdAsyncLN.ObtenerClientePorIdAsync(id.ToString());

            if (response.EsError)
                return NotFound();

            return View("EliminarCliente", response.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarCliente(string clienteId)
        {
            var response = await _eliminarClienteAsyncLN.EliminarClienteAsync(clienteId);

            if (response.EsError)
                return View("EliminarCliente");

            return RedirectToAction("Index");
        }
    }

}

