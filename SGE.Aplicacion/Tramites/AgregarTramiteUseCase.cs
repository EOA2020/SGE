using SGE.Dominio.Tramites;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;


namespace SGE.Aplicacion.Tramites;

public class AgregarTramiteUseCase(ITramiteRepository tramiteRepository,  IAutorizacionService autorizacionService, ActualizacionEstadoExpedienteService actualizacionExpediente, ITimeProvider timeProvider, IUnidadDeTrabajo uow)
{
   
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request, Guid IdUsuario)
    {
        //se verifica que el id del usuario no este vacio
        if(IdUsuario == Guid.Empty)
            throw new AplicacionException("El id no puede estar vacio");

        //se verifica que el usuario tenga permiso
        if(!autorizacionService.PoseeElPermiso(IdUsuario, Permiso.TramiteAlta))
            throw new AutorizacionException("El usuario debe tener permiso");
            
        //creamos el nuevo contenido
        var contenido = new ContenidoTramiteVO(request.Contenido); 
        
        //creamos el nuevo tramite
        var tramite = new Tramite(request.ExpedienteId,contenido,timeProvider.Fecha,timeProvider.Fecha,IdUsuario);

        //lo agregamos
        tramiteRepository.AgregarTramite(tramite);

        uow.GuardarCambios();

        //actualizamos el ultimo expediente
        actualizacionExpediente.ActualizarEstadoExpediente(tramite.UsuarioUltimoCambio,tramite.ExpedienteId);
        
        //retornamos una respuesta
        return new AgregarTramiteResponse(tramite.Id);   
    }

}
