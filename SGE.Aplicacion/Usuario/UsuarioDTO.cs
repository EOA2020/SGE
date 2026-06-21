using SGE.Dominio.Usuarios;

public record class UsuarioDTO(
    Guid Id,
    string Nombre,
    string CorreoElectronico,
    bool EsAdministrador,
    List<string> Permisos
);