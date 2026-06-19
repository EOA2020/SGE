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

        //obtenemos el usuario
        var usuarioAdmin = usuarioRepository.ObtenerUsuarioPorId(IdAdmin);

        //verificamos que no este vacio y si tiene permisos
        if(usuarioAdmin==null || !usuarioAdmin.EsAdministrador)
            throw new AutorizacionException("el usuario no tiene permisos"); 

        //verificamos que el id del usuario no este vacio
        if(request.IdUsuario == Guid.Empty) 
            throw new AplicacionException("El usuario no puede estar vacio");
                
        //lo eliminamos
        usuarioRepository.EliminarUsuario(request.IdUsuario);

        //guardamos los cambios
        uow.GuardarCambios();

        return new EliminarUsuarioResponse(request.IdUsuario);
    }
}