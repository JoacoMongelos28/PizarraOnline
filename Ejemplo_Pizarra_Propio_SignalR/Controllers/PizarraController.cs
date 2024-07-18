using Grupo7_Pizarra_SignalR_Servicios;
using Microsoft.AspNetCore.Mvc;

namespace Grupo7_Pizarra_SignalR.Controllers;

public class PizarraController : Controller
{
    private readonly ISalaServicio _salaServicio;

    public PizarraController(ISalaServicio salaServicio)
    {
        _salaServicio = salaServicio;
    }
    [HttpPost]
    public IActionResult Index(string nombre)
    {
        HttpContext.Session.SetString("nombre", nombre);
        TempData["nombre"] = nombre;
        ViewBag.Salas = _salaServicio.ObtenerTodosLosNombresDeLasSalas();
        return View();
    }

    [HttpPost]
    public IActionResult CrearSala(string sala)
    {
        TempData["nombre"] = HttpContext.Session.GetString("nombre");
        _salaServicio.CrearSala(sala);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult PizarraColaborativa(string? nombre)
    {

        if (string.IsNullOrEmpty(nombre))
        {
            return RedirectToAction("Index", "Home");
        }

        HttpContext.Session.SetString("nombre", nombre);

        TempData["nombre"] = nombre;
        ViewBag.Salas = _salaServicio.ObtenerTodosLosNombresDeLasSalas();
        return View();
    }

    public IActionResult Index()
    {
        TempData["nombre"] = HttpContext.Session.GetString("nombre");
        ViewBag.Salas = _salaServicio.ObtenerTodosLosNombresDeLasSalas();
        return View();
    }
}
