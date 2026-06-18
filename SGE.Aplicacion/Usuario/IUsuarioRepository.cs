using SGE.Dominio.Usuarios;

public interface IUsuarioRepository
{
    void RegistrarUsuario(Usuario usuario);
    void Login(CorreoElectronicoVO correoElectronico);
    void ModificarMisDatosUseCase(Guid idUsurio);
    IEnumerable<Usuario> ListarUsuarios();
    void EliminarUsuario(Guid idUsuario);
    void ModificarPermisosUsuario(Guid idUsuario, string permiso);
    Usuario ObtenerUsuarioPorCorreo(CorreoElectronicoVO correoElectronico);

}
