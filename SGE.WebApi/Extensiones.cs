using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SGE.Aplicacion.Autorizacion;

namespace SGE.WebApi;

public static class Extensiones
{
    public static IServiceCollection AddAutorizacionJWT(
        this IServiceCollection servicios,
        IConfiguration configuracion
    )
    {
        servicios.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opciones =>
            {
                opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //validar quien lo emitio
                    ValidateAudience = true, //validar para quien es
                    ValidateLifetime = true, //validar que no este vencido
                    ValidateIssuerSigningKey = true, //validar la firma criptografica

                    ValidIssuer = configuracion["Jwt:Issuer"],
                    ValidAudience = configuracion["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuracion["Jwt:Key"]!)
                    )
                };
            });
        //agregamos el servicios de auturizacion
        servicios.AddAuthorization();

        servicios.AddScoped<ITokenProvider, JwtTokenProvider>();

        return servicios;
    }

    public static IServiceCollection AddManejoDeExcepciones(this IServiceCollection servicios)
    {
        servicios.AddProblemDetails();
        servicios.AddExceptionHandler<ManejadorDeExceptionesGlobales>();

        return servicios;
    }
}
