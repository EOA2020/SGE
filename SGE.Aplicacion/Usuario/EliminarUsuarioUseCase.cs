using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class EliminarUsuarioUseCase(
    IUsuarioRepository usuarioRepository, 
    IUnidadDeTrabajo uow
)
{
    public EliminarUsuarioResponse Ejecutar(EliminarUsuarioRequest request){

        //chequeamos que el mail sea valido
        CorreoElectronicoVO correoEliminador;
        try{
            correoEliminador = CorreoElectronicoVO.Parse(request.correo1);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //obtenemos el usuario
        var usuarioEliminador = usuarioRepository.ObtenerUsuarioPorCorreo(correoEliminador);

        //chequeamos que el usuario exista
        if(usuarioEliminador==null)
            throw new EntidadNoEncontradaException("EL usuario no existe");

        //chequeamos que sea administrador
        if(!usuarioEliminador.EsAdministrador)
            throw new AutorizacionException("El usuario no es administrador");

        //chequeamos que el mail del usuario a eliminar sea valido
        CorreoElectronicoVO correoEliminado;
        try{
            correoEliminado = CorreoElectronicoVO.Parse(request.correo2);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //obtenemos el usuario a eliminar
        var usuarioEliminado = usuarioRepository.ObtenerUsuarioPorCorreo(correoEliminado);

        //chequeamos que exista
        if(usuarioEliminado == null)
            throw new EntidadNoEncontradaException("El usuario no existe");
        
        //lo eliminamos
        usuarioRepository.EliminarUsuario(usuarioEliminado);

        uow.GuardarCambios();

        return new EliminarUsuarioResponse();
    }
}