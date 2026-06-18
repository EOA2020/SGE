using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;

namespace SGE.Aplicacion;

public static class Extensiones
{
    public static IServiceCollection AddAplicacion(this IServiceCollection servicios)
    {
        servicios.AddScoped<AgregarExpedienteUseCase>();
        servicios.AddScoped<CambiarEstadoExpedienteUseCase>();
        servicios.AddScoped<EliminarExpedienteUseCase>();
        servicios.AddScoped<ModificarCaratulaExpedienteUseCase>();
        servicios.AddScoped<ObtenerPorIdExpedienteUseCase>();
        servicios.AddScoped<ObtenerTodosExpedienteUseCase>();

        servicios.AddScoped<AgregarTramiteUseCase>();
        servicios.AddScoped<EliminarTramiteUseCase>();
        servicios.AddScoped<ModificarTramiteUseCase>();
        servicios.AddScoped<ObtenerPorIdUseCase>();
        servicios.AddScoped<ObtenerTramitePorExpedienteIdUseCase>();

        

        return servicios;
    }
}
    
