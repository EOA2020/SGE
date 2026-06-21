using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ModificarEstadoTramiteUseCase(
    ITramiteRepository tramiteRepository,
    IAutorizacionService autorizacionService,
    ITimeProvider timeProvider,
    ActualizacionEstadoExpedienteService actualizacionEstadoExpedienteService,
    IUnidadDeTrabajo uow
)
{
    public ModificarEstadoTramiteResponse Ejecutar(ModificarEstadoTramiteRequest request, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
            throw new AplicacionException("El id del usuario no puede estar vacio");

        if(!autorizacionService.PoseeElPermiso(idUsuario, Permiso.TramiteModificacion))
            throw new AutorizacionException("No posee los permisos.");

        if(request.IdTramite == Guid.Empty)
            throw new AplicacionException("El id del tramite no puede estar vacio");

        var tramite = tramiteRepository.ObtenerPorId(request.IdTramite);
        if(tramite == null)
            throw new EntidadNoEncontradaException($"El tramite de id {request.IdTramite} no existe.");

        //varificamos que el estado sea valido y lo cambiamos
        var estadoString = request.Estado.Trim();
        if(!Enum.TryParse<EtiquetaTramite>(estadoString, true, out var etiqueta))
            throw new AplicacionException($"Estado inválido: {request.Estado}");

        tramite.ModificarEstado(etiqueta, idUsuario, timeProvider.Fecha);

        //guardamos cambios
        uow.GuardarCambios();

        //actualizamos estado del espediente
        actualizacionEstadoExpedienteService.ActualizarEstadoExpediente(idUsuario, tramite.ExpedienteId);

        //retornamos una respuesta
        return new ModificarEstadoTramiteResponse(tramite.Id);
    }
}
