using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura;

public class UsuarioRepository (SGEContext context): IUsuarioRepository
{
    public void RegistrarUsuario(Usuario usuario)
    {
        //Valudacion usuario no vacio
        if (usuario == null) throw new RepositorioException ("No se puede registrar un usuario vacio");

        //Se agrega usuario al repositorio
        context.Set<Usuario>().Add(usuario);
    }

    public IEnumerable<Usuario> ListarUsuarios()
    {
        return context.Set<Usuario>().ToList();
    }
    public void EliminarUsuario(Guid idUsuario)
    {
        var usuario= context.Set<Usuario>().FirstOrDefault(u => u.Id.Equals(idUsuario));
        //Valida que exista usuario a eliminar
        if (usuario==null) throw new RepositorioException("El usuario a eliminar no existe");

        //Remueve el usuario
        context.Set<Usuario>().Remove(usuario);
    }

    public Usuario ObtenerUsuarioPorCorreo(string correoElectronico)
    {
        var usuario= context.Set<Usuario>().FirstOrDefault(u => u.CorreoElectronico.ToString().Equals(correoElectronico) );

        //Validacion que se encontro usuario
        if (usuario==null) throw new RepositorioException("No se encontro el usuario encontrado");

        //retorna el usuario encontrado
        return usuario;
    }
    
    public Usuario? ObtenerUsuarioPorId(Guid id)    
    {
        return context.Set<Usuario>().FirstOrDefault(u => u.Id.Equals(id));
    }
}
