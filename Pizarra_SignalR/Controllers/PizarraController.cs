using Pizarra_SignalR_Service;
using Microsoft.AspNetCore.Mvc;

namespace Pizarra_SignalR.Controllers
{
    public class PizarraController : Controller
    {
        private readonly ISalaServicio _salaServicio;

        public PizarraController(ISalaServicio salaServicio)
        {
            _salaServicio = salaServicio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PizarraColaborativa(string? nombre)
        {

            if (string.IsNullOrEmpty(nombre))
            {
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetString("nombre", nombre);
            TempData["nombre"] = nombre;
            ViewBag.Salas = _salaServicio.ObtenerTodosLosNombresDeLasSalas();
            return View();
        }
    }
}
