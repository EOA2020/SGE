
public record class ModificarMisDatosRequest(
    Guid IdUsuarioDesdeToken, 
    string NuevoNombre, 
    string NuevoCorreo, 
    string NuevaContrasenaHash
);