using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class EliminarUsuarioUseCase(
    IUsuarioRepository usuarioRepository, 
    IUnidadDeTrabajo uow
)
{
    public EliminarUsuarioResponse Ejecutar(EliminarUsuarioRequest request, Guid IdAdmin){

        var usuarioAdmin = usuarioRepository.ObtenerUsuarioPorId(IdAdmin);

        if(usuarioAdmin==null || !usuarioAdmin.EsAdministrador)
            throw new AutorizacionException("el usuario no tiene permisos"); 

        if(request.IdUsuario == Guid.Empty) 
            throw new AplicacionException("El usuario no puede estar vacio");
                
        //lo eliminamos
        usuarioRepository.EliminarUsuario(request.IdUsuario);

        uow.GuardarCambios();

        return new EliminarUsuarioResponse(request.IdUsuario);
    }
}