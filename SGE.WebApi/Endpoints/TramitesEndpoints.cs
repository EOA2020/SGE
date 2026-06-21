using System.Security.Claims;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Tramites;

namespace SGE.WebApi.Endpoints;

public static class TramitesEndpoints
{
    public static void MapTramitesEndpoints(this IEndpointRouteBuilder app)
    {
        var tramitesApi = app.MapGroup("/api/tramites").WithTags("Gestion de Tramites");

        //crear un tramite
        tramitesApi.MapPost("/", (
            AgregarTramiteRequest request,
            ClaimsPrincipal user,
            AgregarTramiteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Created($"/api/tramites/{response.IdTramite}", response);
        })
        .RequireAuthorization()
        .WithName("AgregarTramite")
        .WithSummary("Crea un nuevo trámite")
        .WithDescription(
            "Registra un nuevo trámite asociado a un expediente existente, utilizando el identificador del expediente y el contenido proporcionado."
        );

        //obtener todos los tramites por el id de un expediente
        tramitesApi.MapGet("/id-expediente/{id:guid}", (
            Guid id,
            ObtenerTramitePorExpedienteIdUseCase useCase
        ) =>
        {
            var request = new ObtenerTramitePorExpedienteIdRequest(id);
            var response = useCase.Ejecutar(request);
            
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ListarTramitesPorExpediente")
        .WithSummary("Obtiene los trámites de un expediente")
        .WithDescription(
            "Devuelve la lista de trámites asociados al expediente identificado por el ID proporcionado."
        );

        //obtener un tramite por su id
        tramitesApi.MapGet("/{id:guid}", (
            Guid id,
            ObtenerPorIdUseCase useCase
        ) =>
        {
            var request = new ObtenerPorIdResquest(id);
            var response = useCase.Ejecutar(request);

            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ObtenerTramitePorId")
        .WithSummary("Obtiene un trámite por su identificador")
        .WithDescription(
            "Devuelve la información de un trámite específico a partir de su identificador único."
        );

        //modificar el contenido de un tramite
        tramitesApi.MapPut("/modificar-contenido", (
            ModificarTramiteRequest request,
            ClaimsPrincipal user,
            ModificarTramiteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ModificarContenidoTramite")
        .WithSummary("Modifica el contenido de un trámite")
        .WithDescription(
            "Permite actualizar el contenido de un trámite existente a partir de su identificador."
        );

        //modificar el estado de un tramite
        tramitesApi.MapPut("/modificar-estado", (
            ModificarEstadoTramiteRequest request,
            ClaimsPrincipal user, 
            ModificarEstadoTramiteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ModificarEstadoTramite")
        .WithSummary("Modifica el estado de un trámite")
        .WithDescription(
            "Permite actualizar el estado de un trámite existente indicando su identificador y el nuevo estado."
        );

        //eliminar un tramite
        tramitesApi.MapDelete("/{id:guid}", (
            Guid id,
            ClaimsPrincipal user,
            EliminarTramiteUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!); 

            var request = new EliminarTramiteRequest(id);
            var response = useCase.Ejecutar(request, idUsuario);

            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("EliminarTramite")
        .WithSummary("Elimina un trámite")
        .WithDescription(
            "Elimina del sistema el trámite identificado por el ID proporcionado."
        );
    }
}
