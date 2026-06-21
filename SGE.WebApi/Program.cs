using Scalar.AspNetCore;
using SGE.Aplicacion;
using SGE.Infraestructura;
using SGE.WebApi;
using SGE.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//Registros de servicios en el contenedor DI
builder.Services.AddOpenApi()
                .AddAplicacion()
                .AddInfraestructura(builder.Configuration)
                .AddAutorizacionJWT(builder.Configuration)
                .AddManejoDeExcepciones();

var app = builder.Build();

//agregamos el middleware al principio del pipeline
app.UseExceptionHandler();

//agregamos el midleware para la autenticacion
app.UseAuthentication();

//agregamos el midleware de protecion de rutas
app.UseAuthorization();

if (app.Environment.IsDevelopment()) //solo si estamos en modo desarrollo
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// ====================================================================
//          INICIALIZACIÓN DE LA BASE DE DATOS 
// ====================================================================
// Como SGEContext se registró como "Scoped", su ciclo de vida natural 
// exige que exista dentro de una petición HTTP. Como el servidor recién 
// arranca y aún no hay peticiones, "simulamos" una creando un Scope 
// temporal. Así podemos pedirle el contexto al contenedor de forma segura.

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SGEContext>();
    SGESqlite.Inicializar(context);
}

app.MapGet("/", () => "La API de SGE esta funcionando correctamente!");

app.MapLoginEndpoint();
app.MapUsuariosEndpoints();
app.MapExpedientesEndpoints();
app.MapTramitesEndpoints();

app.Run();
