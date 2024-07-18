using Grupo7_Pizarra_SignalR_Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo7_Pizarra_SignalR_Servicios;

public interface ISalaServicio
{
    Task<Sala> CrearSalaAsync(string nombre);
    Task<Sala?> ObtenerSalaPorIdAsync(int id);
    Task<Sala?> ObtenerSalaPorNombreAsync(string sala);
    Task<List<Sala>> ObtenerTodasLasSalasAsync();
    Task<List<string>> ObtenerTodosLosNombresDeLasSalasAsync();
    List<string> ObtenerTodosLosNombresDeLasSalas();
    List<Sala> ObtenerTodasLasSalas();
    void CrearSala(string nombre);
}
public class SalaServicio : ISalaServicio
{
    private readonly PizarraContext _context;
    public SalaServicio(PizarraContext context)
    {
        _context = context;
    }

    public void CrearSala(string nombre)
    {
        _context.Salas.Add(new Sala { NombreSala = nombre });
        _context.SaveChanges();
    }

    public async Task<Sala> CrearSalaAsync(string nombre)
    {
        var sala = new Sala { NombreSala = nombre };
        /*var sala = new Sala
        {
            NombreSala = nombre
        };*/
        _context.Salas.Add(sala);
        await _context.SaveChangesAsync();
        return sala;
    }

    public async Task<Sala?> ObtenerSalaPorIdAsync(int id)
    {
        return await _context.Salas.FindAsync(id);
    }

    public async Task<Sala?> ObtenerSalaPorNombreAsync(string sala)
    {
        return await _context.Salas.FirstOrDefaultAsync(s => s.NombreSala.Equals(sala));
        
    }

    public List<Sala> ObtenerTodasLasSalas()
    {
        return _context.Salas.ToList();
    }

    public async Task<List<Sala>> ObtenerTodasLasSalasAsync()
    {
        return await _context.Salas.ToListAsync();
    }

    public List<string> ObtenerTodosLosNombresDeLasSalas()
    {
        var salas = ObtenerTodasLasSalas();
        List<string> nombres = new List<string>();
        foreach (var sala in salas)
        {
            nombres.Add(sala.NombreSala);
        }
        return nombres;
    }

    public async Task<List<string>> ObtenerTodosLosNombresDeLasSalasAsync()
    {
        var salas = await ObtenerTodasLasSalasAsync();
        List<string> nombres = new List<string>();
        foreach (var sala in salas)
        {
            nombres.Add(sala.NombreSala);
        }
        return nombres;
    }
}
