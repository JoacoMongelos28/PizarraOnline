﻿using Pizarra_SignalR_Data.Entidades;
using Pizarra_SignalR_Service;
using Microsoft.AspNetCore.SignalR;

namespace Pizarra_SignalR.Hubs;

public class PizarraHub : Hub
{
    private static Dictionary<string, HashSet<string>> salas = new Dictionary<string, HashSet<string>>();
    private static Dictionary<string, List<string>> dibujosPorSala = new Dictionary<string, List<string>>();
    private readonly IDibujoServicio _dibujoServicio;
    private readonly ISalaServicio _salaServicio;
    public bool primeraconexion = true;

    public PizarraHub(IDibujoServicio dibujoServicio, ISalaServicio salaServicio, PizarraContext context)
    {
        _dibujoServicio = dibujoServicio;
        _salaServicio = salaServicio;
    }

    public async Task UnirseASala(string sala)
    {
        ObtenerTodasLasSalas();
        if (!salas.ContainsKey(sala))
        {
            salas[sala] = new HashSet<string>();
            var nuevaSala = await _salaServicio.CrearSalaAsync(sala);
            dibujosPorSala[sala] = new List<string>();
        }

        var usuario = Context.Items["Usuario"].ToString();
        salas[sala].Add(usuario);
        await Groups.AddToGroupAsync(Context.ConnectionId, sala);
        var idSala = (await _salaServicio.ObtenerSalaPorNombreAsync(sala)).IdSala;
        var dibujos = await _dibujoServicio.ObtenerDibujosAsync(idSala);
        dibujosPorSala[sala] = dibujos;
        await EnviarDibujoActual(sala, Context.ConnectionId);
        primeraconexion = false;
        await ActualizarUsuariosEnSala(sala);
    }

    private void ObtenerTodasLasSalas()
    {
        Task<List<string>> salasBD = _salaServicio.ObtenerTodosLosNombresDeLasSalasAsync();
        Dictionary<string, HashSet<string>> salasHub = salas;
        foreach (var sala in salasBD.Result)
        {
            if (salasHub.ContainsKey(sala))
            {
                salas[sala] = salasHub[sala];
                continue;
            }
            salas[sala] = new HashSet<string>();
        }
    }

    public async Task SalirDeSala(string sala)
    {
        var usuario = Context.Items["Usuario"].ToString();
        if (salas.ContainsKey(sala))
        {
            salas[sala].Remove(usuario);
            if (salas[sala].Count == 0)
            {
                salas.Remove(sala);
                dibujosPorSala.Remove(sala);
            }
        }

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, sala);
        await ActualizarUsuariosEnSala(sala);
    }

    public async Task Dibujar(string sala, string data)
    {
        if (dibujosPorSala.ContainsKey(sala))
        {
            dibujosPorSala[sala].Add(data);
        }

        var salaEntidad = await _salaServicio.ObtenerSalaPorNombreAsync(sala);
        if (salaEntidad != null)
        {
            await _dibujoServicio.GuardarDibujoAsync(salaEntidad.IdSala, data);
        }

        await Clients.OthersInGroup(sala).SendAsync("dibujarEnPizarra", data);
    }

    public async Task<List<string>> ObtenerDibujos(int idSala)
    {
        return await _dibujoServicio.ObtenerDibujosAsync(idSala);
    }

    public async Task EnviarMensaje(string sala, string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            var usuario = Context.Items["Usuario"];
            await Clients.Group(sala).SendAsync("RecibirMensaje", usuario, message);
        }

    }

    public async Task BorrarDibujos(string sala)
    {
        if (salas.ContainsKey(sala))
        {
            var salaEncontrada = await _salaServicio.ObtenerSalaPorNombreAsync(sala);
            _dibujoServicio.BorrarDibujos(salaEncontrada.IdSala);
            await Clients.Group(sala).SendAsync("LimpiarPizarra");
        }
    }

    private async Task ActualizarUsuariosEnSala(string sala)
    {
        if (salas.ContainsKey(sala))
        {
            var usuarios = new List<string>(salas[sala]);
            await Clients.Group(sala).SendAsync("ActualizarUsuarios", usuarios);
        }
    }

    private async Task EnviarDibujoActual(string sala, string connectionId)
    {
        if (dibujosPorSala.ContainsKey(sala))
        {
            var dibujos = dibujosPorSala[sala];
            foreach (var dibujo in dibujos)
            {
                await Clients.Client(connectionId).SendAsync("dibujarEnPizarra", dibujo);
            }
        }
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var usuario = httpContext.Session.GetString("nombre");

        if (!string.IsNullOrEmpty(usuario))
        {
            Context.Items["Usuario"] = usuario;
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(System.Exception exception)
    {
        var usuario = Context.Items["Usuario"].ToString();
        foreach (var sala in salas)
        {
            if (sala.Value.Contains(usuario))
            {
                sala.Value.Remove(usuario);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, sala.Key);
                await Clients.Group(sala.Key).SendAsync("UsuarioDesconectado", usuario);
                await ActualizarUsuariosEnSala(sala.Key);

                if (sala.Value.Count == 0)
                {
                    salas.Remove(sala.Key);
                    dibujosPorSala.Remove(sala.Key);
                }

                break;
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}