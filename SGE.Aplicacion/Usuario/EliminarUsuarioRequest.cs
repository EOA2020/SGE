using SGE.Dominio.Usuarios;

public record class EliminarUsuarioRequest(
    string correo1,
    string correo2
);