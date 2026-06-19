using SGE.Dominio.Usuarios;

public record class ListarUsuariosResponse(
    IEnumerable<UsuarioDTO> Usuarios
);