using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class EliminarExpedienteUseCase(ITramiteRepository tramiteRepository,  IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionExpediente)
{
    public EliminarTramiteUseCaseResponse Ejecutar(EliminarTramiteUseCaseRequest request)
    {
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El id del usuario no puede estar vacio");

        if(!autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.TramiteBaja))
            throw new AutorizacionException("El usuario no posee el permiso");

        if(request.IdTramite == Guid.Empty)
            throw new AplicacionException("El id del tramite no puede estar vacio");

        var tramite = tramiteRepository.ObtenerPorId(request.IdTramite);
        if(tramite == null)
            throw new EntidadNoEncontradaException("El tramite no existe");

        tramiteRepository.EliminarTramite(tramite);
        
        actualizacionExpediente.ActualizarEstadoExpediente(request.IdUsuario,tramite.ExpedienteId);

        return new EliminarTramiteUseCaseResponse();
        
    } 
}
