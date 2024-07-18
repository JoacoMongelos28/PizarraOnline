using Ejemplo_Pizarra_Propio_SignalR.Hubs;
using Grupo7_Pizarra_SignalR_Data.Entidades;
using Grupo7_Pizarra_SignalR_Servicios;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<PizarraContext>();
builder.Services.AddScoped<IDibujoServicio, DibujoServicio>();
builder.Services.AddScoped<ISalaServicio, SalaServicio>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
 {
     options.IdleTimeout = TimeSpan.FromSeconds(10);
     options.Cookie.HttpOnly = true;
     options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});

// Add Quartz.NET hosted service
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<PizarraHub>("/pizarraHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
