using SGE.Dominio.Usuarios;

public interface IUsuarioRepository
{
    void RegistrarUsuario(Usuario usuario);
    IEnumerable<Usuario> ListarUsuarios();
    void EliminarUsuario(Guid idUsuario);
    Usuario? ObtenerUsuarioPorCorreo(CorreoElectronicoVO correoElectronico);
    Usuario? ObtenerUsuarioPorId(Guid id);

}
