namespace SGE.Aplicacion.Tramites;

public record class ModificarEstadoTramiteRequest(
    Guid IdTramite,
    string Estado
);
