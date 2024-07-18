using Ejemplo_Pizarra_Propio_SignalR.Models;
using Grupo7_Pizarra_SignalR_Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ejemplo_Pizarra_Propio_SignalR.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISalaServicio _salaServicio;

    public HomeController(ILogger<HomeController> logger, ISalaServicio salaServicio)
    {
        _logger = logger;
        _salaServicio = salaServicio;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string sala)
    {
        var nombreUsuario = HttpContext.Session.GetString("nombre");
        TempData["nombre"] = nombreUsuario;
        _salaServicio.CrearSala(sala);
        ViewBag.Salas =  _salaServicio.ObtenerTodosLosNombresDeLasSalas();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}