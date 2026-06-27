namespace SGE.WebApi.Endpoints;

public static class LoginEndpoints
{
    public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login", (LoginRequest request, LoginUseCase useCase) =>
        {
            var response = useCase.Ejecutar(request);
            return Results.Ok(response);
        })
        .WithTags("Autenticacion.")
        .WithName("IniciarSesion")
        .WithSummary("Inicia sesión en el sistema")
        .WithDescription(
            "Autentica a un usuario mediante su correo electrónico y contraseña. " +
            "Si las credenciales son válidas, se genera y devuelve un token de autenticación para acceder a los recursos protegidos."
        );
    }
}
