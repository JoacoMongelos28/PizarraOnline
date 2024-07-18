using Ejemplo_Pizarra_Propio_SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace Grupo7_Pizarra_SignalR.Job;

public class SignalRJob : IJob
{
    private readonly IHubContext<PizarraHub> _hubContext;

    public SignalRJob(IHubContext<PizarraHub> hubContext)
    {
        _hubContext = hubContext;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        var sala = context.JobDetail.Key.Name;
        await _hubContext.Clients.Group(sala).SendAsync("GuardarImagen");

    }
}
