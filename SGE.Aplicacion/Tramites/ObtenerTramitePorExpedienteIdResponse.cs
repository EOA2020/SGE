using System;

namespace SGE.Aplicacion.Tramites;

public record class ObtenerTramitePorExpedienteIdResponse(IEnumerable<TramiteDTO> Tramites)
{

}
