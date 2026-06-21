using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class EliminarTramiteUseCase(ITramiteRepository tramiteRepository,  IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionExpediente, IUnidadDeTrabajo uow)
{
    public EliminarTramiteResponse Ejecutar(EliminarTramiteRequest request,  Guid IdUsuario)
    {
        //verificamos que el id no este vacio
        if(IdUsuario == Guid.Empty)
            throw new AplicacionException("El id del usuario no puede estar vacio");

        //verificamos que tenga permisos
        if(!autorizacionService.PoseeElPermiso(IdUsuario, Permiso.TramiteBaja))
            throw new AutorizacionException("El usuario no posee el permiso");

        //verificamos que el id del tramite no este vacio
        if(request.IdTramite == Guid.Empty)
            throw new AplicacionException("El id del tramite no puede estar vacio");

        //obtenemos el tramite
        var tramite = tramiteRepository.ObtenerPorId(request.IdTramite);

        //verificamos que exista
        if(tramite == null)
            throw new EntidadNoEncontradaException("El tramite no existe");

        //lo eliminamos
        tramiteRepository.EliminarTramite(tramite.Id);
        
        uow.GuardarCambios();

        //actualizamos el aultimo expediente
        actualizacionExpediente.ActualizarEstadoExpediente(IdUsuario,tramite.ExpedienteId);

        //retornamos una respuesta
        return new EliminarTramiteResponse(request.IdTramite);
        
    } 
}
