using System;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ModificarTramiteUseCase(ITramiteRepository tramiteRepository, IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionExpediente)
{
    private readonly ITramiteRepository _tramiteRepository = tramiteRepository;
    private readonly IAutorizacionService _autorizacionService = autorizacionService;
    private readonly ActualizacionEstadoExpedienteService _actualizacionExpediente = actualizacionExpediente;
public ModificarTramiteResponse Ejecutar(ModificarTramiteRequest request)
    {
        
        if(request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El id no puede estar vacio");

        if(!_autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.TramiteModificacion))
            throw new AutorizacionException("El usuario debe tener permiso");
        
        var tramite = _tramiteRepository.ObtenerPorId(request.TramiteId);
        if (tramite == null)
            throw new Exception("El trámite que intenta modificar no existe");

        var contenido = new ContenidoTramite(request.Contenido);
        tramite.ModificarContenido(contenido, request.IdUsuario);
 
        _tramiteRepository.ModificarTramite(tramite);
        _actualizacionExpediente.ActualizarEstadoExpediente(request.IdUsuario,tramite.ExpedienteId);

        return new ModificarTramiteResponse();
    }


}