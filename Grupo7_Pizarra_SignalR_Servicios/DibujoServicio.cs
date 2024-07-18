using Grupo7_Pizarra_SignalR_Data.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Grupo7_Pizarra_SignalR_Servicios;

public interface IDibujoServicio
{
    Task GuardarDibujoAsync(int idSala, string data);
    Task<List<string>> ObtenerDibujosAsync(int idSala);
    void BorrarDibujos(int idSala);
}
public class DibujoServicio : IDibujoServicio
{
    private readonly PizarraContext _context;

    public DibujoServicio(PizarraContext context)
    {
        _context = context;
    }

    public void BorrarDibujos(int idSala)
    {
        var dibujos = _context.Dibujos.Where(d => d.IdSala == idSala).ToList();
        if (dibujos != null)
        {
            _context.Dibujos.RemoveRange(dibujos);
            _context.SaveChanges();

        }
    }

    public async Task GuardarDibujoAsync(int idSala, string data)
    {
        var dibujo = new Dibujo
        {
            Dibujo1 = data,
            IdSala = idSala
        };
        _context.Dibujos.Add(dibujo);
        await _context.SaveChangesAsync();
    }

    public async Task<List<string>> ObtenerDibujosAsync(int idSala)
    {
        return await _context.Dibujos.Where(d => d.IdSala == idSala)
            .Select(d => d.Dibujo1).ToListAsync();
    }
}
