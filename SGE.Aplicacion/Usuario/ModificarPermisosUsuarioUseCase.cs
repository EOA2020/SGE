using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class ModificarPermisosUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo uow)
{
    public ModificarPermisosUsuarioResponse Ejecutar(ModificarPermisosUsuarioRequest request)
    {

        //chequeamos que el correo sea valido
        CorreoElectronicoVO correoModificador;
        try{
            correoModificador = CorreoElectronicoVO.Parse(request.correo1);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //obteemos su usuario
        var usuarioModificador = usuarioRepository.ObtenerUsuarioPorCorreo(correoModificador);

        //chequeamos que no este vacio
        if(usuarioModificador==null)
            throw new EntidadNoEncontradaException("EL usuario no existe");

        //chequemaos que sea administrador
        if(!usuarioModificador.EsAdministrador)
            throw new AutorizacionException("El usuario no es administrador");

        //chequeamos que el correo del usuario a modificar sea valido
        CorreoElectronicoVO correoModificado;
        try{
            correoModificado = CorreoElectronicoVO.Parse(request.correo2);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //obtenemos su usuario
        var usuarioModificado = usuarioRepository.ObtenerUsuarioPorCorreo(correoModificado);

        //verificamos que no este vacio
        if(usuarioModificado == null)
            throw new EntidadNoEncontradaException("El usuario no existe");

        //creamos una nueva lista de permisos con los permisos del usuario
        var nuevaListaPermisos = new List<string>(usuarioModificado.Permisos);

        // si la lista contiene el permiso ingresado
        if(nuevaListaPermisos.Contains(request.permiso))
            //se lo quita
            nuevaListaPermisos.Remove(request.permiso);
        else
            //sino, se lo agrega
            nuevaListaPermisos.Add(request.permiso);

        //se agrega o se quita el permiso
        usuarioModificado.ModificarPermiso(nuevaListaPermisos);

        //se guardan los cambios
        uow.GuardarCambios();

        return new ModificarPermisosUsuarioResponse();
    }
}