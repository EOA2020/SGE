using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SGE.Aplicacion.Usuarios;

namespace SGE.WebApi.Endpoints;

public static class UsuariosEndpoints
{
    public static void MapUsuariosEndpoints(this IEndpointRouteBuilder app)
    {
        var usuariosApi = app.MapGroup("/api/usuarios").WithTags("Gestion de Usuarios");

        //crear un usuario
        usuariosApi.MapPost("/", (
            RegistrarUsuarioRequest request,
            RegistrarUsuarioUseCase useCase
        ) =>
        {
            var response = useCase.Ejecutar(request);
            return Results.Created($"/api/usuarios/{response.Id}", response);
        })
        .WithName("RegistrarUsuario")
        .WithSummary("Registra un nuevo usuario")
        .WithDescription(
            "Crea un nuevo usuario en el sistema a partir de su nombre, correo electrónico y contraseña. " +
            "Si los datos son válidos, el usuario es almacenado y se devuelve una respuesta exitosa."
        );

        //listar todos los usuarios
        usuariosApi.MapGet("/", (
            ClaimsPrincipal user,
            ListarUsuariosUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(new ListarUsuariosRequest(), idUsuario);
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ListarUsuarios")
        .WithSummary("Obtiene la lista de usuarios")
        .WithDescription(
            "Devuelve la información de todos los usuarios registrados en el sistema."
        );

        //modificar los permisos de los usuarios
        usuariosApi.MapPut("/permisos", (
            ModificarPermisosUsuarioRequest request,
            ClaimsPrincipal user,
            ModificarPermisosUsuarioUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);            
        })
        .RequireAuthorization()
        .WithName("ModificarMisDatos")
        .WithSummary("Modifica los datos del usuario autenticado")
        .WithDescription(
            "Permite actualizar el nombre, correo electrónico y/o contraseña del usuario autenticado. " +
            "Solo se modificarán los campos proporcionados en la solicitud."
        );

        //modificar los datos del usuario
        usuariosApi.MapPut("/", (
            ModificarMisDatosRequest request,
            ClaimsPrincipal user,
            ModificarMisDatosUseCase useCase
        )=>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("ModificarPermisosUsuario")
        .WithSummary("Modifica los permisos de un usuario")
        .WithDescription(
            "Permite agregar o modificar un permiso de un usuario específico a partir de su identificador. " +
            "La solicitud debe incluir el ID del usuario y el permiso que se desea asignar."
        );

        //eliminar a un usuario
        usuariosApi.MapDelete("/{id:guid}", (
            Guid Id,
            ClaimsPrincipal user,
            EliminarUsuarioUseCase useCase
        ) =>
        {
            var usuarioIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(usuarioIdString!);

            var request = new EliminarUsuarioRequest(Id);
            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);
        })
        .RequireAuthorization()
        .WithName("EliminarUsuario")
        .WithSummary("Elimina un usuario")
        .WithDescription(
            "Elimina del sistema al usuario identificado por el ID proporcionado."
        );
    }   
}
