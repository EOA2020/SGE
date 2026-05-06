using System;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;

public class ObtenerPorExpedienteIdUseCase(ITramiteRepository tramiteRepository, IExpedienteRepository expedienteRepository)
{
    public ObtenerPorExpedienteIdResponse Ejecutar(ObtenerPorExpedienteIdRequest request)
    {
        if (request.IdExpediente == Guid.Empty)
            throw new AplicacionException("El id del expediente no puede estar vacio");

        var expediente = expedienteRepository.ObtenerPorId(request.IdExpediente);

        if(expediente == null)
            throw new EntidadNoEncontradaException("El expediente no existe");

        var tramites = tramiteRepository.ObtenerPorExpedienteId(request.IdExpediente);

        var tramitesDTO = new List<TramiteDTO>();

        foreach (var tramite in tramites)
    {
        var dto = new TramiteDTO(
            Id: tramite.Id,
            ExpedienteId: tramite.ExpedienteId,
            Etiqueta: tramite.Etiqueta.ToString(), 
            Contenido: tramite.Contenido.Valor,    
            FechaCreacion: tramite.FechaCreacion,
            FechaUltimaModificacion: tramite.FechaUltimaModificacion,
            UsuarioUltimoCambio: tramite.UsuarioUltimoCambio
        );
        
        
        tramitesDTO.Add(dto);
    }
       
        return new ObtenerPorExpedienteIdResponse(tramitesDTO);
    }
}
