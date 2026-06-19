using Microsoft.AspNetCore.Mvc;
using SGE.Aplicacion.Usuarios;

namespace SGE.WebApi.Endpoints;

public static class UsuariosEndpoints
{
    public static void MapUsuariosEndpoints(this IEndpointRouteBuilder app)
    {
        var usuariosApi = app.MapGroup("/api/usuarios").WithTags("Gestion de Usuarios");

        usuariosApi.MapPost("/", (
            RegistrarUsuarioRequest request,
            RegistrarUsuarioUseCase useCase
        ) =>
        {
            var response = useCase.Ejecutar(request);
            return Results.Created($"/api/usuarios/{response.Id}", response);
        });

        usuariosApi.MapPut("/", (
            ModificarMisDatosRequest request,
            [FromHeader(Name = "X-usuario-Id")] Guid idUsuario,
            ModificarMisDatosUseCase useCase
        )=>
        {
            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);
        });
    }   
}
