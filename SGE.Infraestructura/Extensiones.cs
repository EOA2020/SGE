using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;

namespace SGE.Infraestructura;

public static class Extensiones
{
    public static IServiceCollection AddInfraestructura(
        this IServiceCollection servicios,
        IConfiguration configuration
    )
    {
        //Base de datos
        var connectionString = configuration.GetConnectionString("SGEDB");
        servicios.AddDbContext<SGEContext>(opciones => opciones.UseSqlite(connectionString));

        //seguridad (autorizacion)
        servicios.AddScoped<IAutorizacionService, AutorizacionService>();

        //patron Unit of Work y Repositorios
        servicios.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
        servicios.AddScoped<IExpedienteRepository, ExpedienteRepository>();
        servicios.AddScoped<ITramiteRepository, TramiteRepository>();
        servicios.AddScoped<IUsuarioRepository, UsuarioRepository>();
        servicios.AddScoped<ITimeProvider, SystemTimeProvider>();

        return servicios;
    }   
}
