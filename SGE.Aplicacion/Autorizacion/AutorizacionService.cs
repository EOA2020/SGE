using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Autorizacion;

public class AutorizacionService(
    IUsuarioRepository usuarioRepository
): IAutorizacionService
{
    public bool PoseeElPermiso(Guid idUsuario, Permiso permiso)
    {
        var usuario = usuarioRepository.ObtenerUsuarioPorId(idUsuario);
        if(usuario == null) 
            throw new EntidadNoEncontradaException($"El usuario con id {idUsuario} no existe!");

        return usuario.Permisos.Any(p =>
                  Enum.TryParse<Permiso>(p, out var pEnum) &&
                  pEnum == permiso);
    }
}
