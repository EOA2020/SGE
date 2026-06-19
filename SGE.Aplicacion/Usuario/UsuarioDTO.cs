using SGE.Dominio.Usuarios;

public record class UsuarioDTO(
    Guid Id,
    string Nombre,
    CorreoElectronicoVO CorreoElectronico,
    string ContrasenaHash,
    bool EsAdministrador,
    List<string> Permisos
);