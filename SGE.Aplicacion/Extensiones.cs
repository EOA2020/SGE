using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Expedientes;

namespace SGE.Aplicacion;

public static class Extensiones
{
    public static IServiceCollection AddAplicacion(this IServiceCollection servicios)
    {
        servicios.AddScoped<AgregarExpedienteUseCase>();

        return servicios;
    }
}
    
