using System.Security.Claims;
using SGE.Aplicacion.Expedientes;

namespace SGE.WebApi.Endpoints;

public static class ExpedientesEndpoints
{
    public static void MapExpedientesEndpoints(this IEndpointRouteBuilder app)
    {
        var expedientesApi = app.MapGroup("/api/expedientes").WithTags("Gestion de Expedientes");

        //obtener todos los expedientes
        expedientesApi.MapGet("/", (
            ObtenerTodosExpedienteUseCase useCase
        ) =>
        {
            var response = useCase.Ejecutar(new ObtenerTodosExpedienteRequest());
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ListarExpedientes")
        .WithSummary("Obtiene la lista de expedientes")
        .WithDescription(
            "Devuelve la información de todos los expedientes registrados en el sistema."
        );

        //obtener un expediente por su id
        expedientesApi.MapGet("/{id:guid}", (
            Guid id,
            ObtenerPorIdExpedienteUseCase useCase
        ) =>
        {
            var request = new ObtenerPorIdExpedienteRequest(id);
            var response = useCase.Ejecutar(request);

            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ObtenerExpedientePorId")
        .WithSummary("Obtiene un expediente por su identificador")
        .WithDescription(
            "Devuelve la información de un expediente específico a partir de su identificador único."
        );

        //crear un expediente
        expedientesApi.MapPost("/", (
            AgregarExpedienteRequest request,
            ClaimsPrincipal user,
            AgregarExpedienteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Created($"/api/expedientes/{response.IdExpediente}", response);
        })
        .RequireAuthorization()
        .WithName("AgregarExpediente")
        .WithSummary("Crea un nuevo expediente")
        .WithDescription(
            "Crea un nuevo expediente a partir de la carátula proporcionada y lo registra en el sistema."
        );

        //cambiar el estado manual de un expediente
        expedientesApi.MapPut("/cambiar-estado", (
            CambiarEstadoExpedienteRequest request,
            ClaimsPrincipal user,
            CambiarEstadoExpedienteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("CambiarEstadoExpediente")
        .WithSummary("Modifica el estado de un expediente")
        .WithDescription(
            "Permite actualizar el estado de un expediente existente indicando su identificador y el nuevo estado."
        );

        //cambiar la caratula de un expediente
        expedientesApi.MapPut("/cambiar-caratula", (
            ModificarCaratulaExpedienteRequest request,
            ClaimsPrincipal user,
            ModificarCaratulaExpedienteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response); 
        })
        .RequireAuthorization()
        .WithName("ModificarCaratulaExpediente")
        .WithSummary("Modifica la carátula de un expediente")
        .WithDescription(
            "Permite actualizar la carátula de un expediente existente a partir de su identificador."
        );

        //eliminar un expediente
        expedientesApi.MapDelete("/{id:guid}", (
            Guid id,
            ClaimsPrincipal user, 
            EliminarExpedienteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var request = new EliminarExpedienteRequest(id);
            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response); 
        })
        .RequireAuthorization()
        .WithName("EliminarExpediente")
        .WithSummary("Elimina un expediente")
        .WithDescription(
            "Elimina del sistema el expediente identificado por el ID proporcionado."
        );
    }   
}
