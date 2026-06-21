using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;

public class ModificarPermisosUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo uow)
{
    public ModificarPermisosUsuarioResponse Ejecutar(ModificarPermisosUsuarioRequest request, Guid IdAdmin)
    {
        //verificamos que el id del usuario no este vacio
        if(IdAdmin == Guid.Empty)
            throw new AplicacionException("El id no puede estar vacio");

        //obteemos su usuario
        var usuarioModificador = usuarioRepository.ObtenerUsuarioPorId(IdAdmin);

        //chequeamos que no este vacio
        if(usuarioModificador==null)
            throw new EntidadNoEncontradaException("EL usuario no existe");

        //chequemaos que sea administrador
        if(!usuarioModificador.EsAdministrador)
            throw new AutorizacionException("El usuario no es administrador");

        //chequeamos que el correo del usuario a modificar sea valido
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El id del usuario no puede estar vacio");

        //obtenemos su usuario
        var usuarioModificado = usuarioRepository.ObtenerUsuarioPorId(request.IdUsuario);

        //verificamos que no este vacio
        if(usuarioModificado == null)
            throw new EntidadNoEncontradaException("El usuario no existe");

        //hacemos una copia de la lista con sus permisos
        var nuevaListaPermisos = new List<string>(usuarioModificado.Permisos);

        try
        {
            //verificamos que el permiso sea valido
            Enum.TryParse(request.Permiso,true, out Permiso permisoNuevo);

            //verificamos que el usuario contenga el nuevo permiso  
            if(nuevaListaPermisos.Contains(permisoNuevo.ToString()))
            {
                //si tiene el permiso, se lo sacamos
                nuevaListaPermisos.Remove(permisoNuevo.ToString());
            }
            else
            {
                //si no lo tiene, se lo agregamos
                nuevaListaPermisos.Add(permisoNuevo.ToString());
            }
        }
        catch
        {
            throw new AplicacionException("El permiso no es valido");
        }
        
        //se agrega o se quita el permiso
        usuarioModificado.ModificarPermiso(nuevaListaPermisos);

        //se guardan los cambios
        uow.GuardarCambios();

        return new ModificarPermisosUsuarioResponse(request.IdUsuario);
    }
}