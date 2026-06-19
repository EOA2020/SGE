using SGE.Dominio.Usuarios;

public interface IUsuarioRepository
{
    void RegistrarUsuario(Usuario usuario);
    void Login(string correoElectronico, string contrasena);
    void ModificarMisDatos(Guid idUsurio);
    IEnumerable<UsuarioDTO> ListarUsuarios();
    void EliminarUsuario(Guid idUsuario);
    void ModificarPermisosUsuario(List<string> permiso);
    Usuario ObtenerUsuarioPorCorreo(CorreoElectronicoVO correoElectronico);
    Usuario? ObtenerUsuarioPorId(Guid id);

}
