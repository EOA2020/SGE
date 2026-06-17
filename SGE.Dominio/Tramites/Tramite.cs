using SGE.Dominio.Comun;
namespace SGE.Dominio.Tramites;

public class Tramite
{
    public Guid Id { get; private set; }   
    public Guid ExpedienteId { get; private set; }
    public EtiquetaTramite Etiqueta { get; private set; } 
    public ContenidoTramiteVO Contenido { get; private set; } = null!; 
    public DateTime FechaCreacion { get;  private set;}
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }

    protected Tramite(){}

    public Tramite(Guid expedienteId, ContenidoTramiteVO contenido, DateTime fechaCreacion, 
    DateTime fechaUltimaModificacion, Guid idUsuario)
    {
        //lanzamos una excepcion de tipo dominio si no se cumplen cierta logica de negocio
        if(idUsuario == Guid.Empty)
            throw new DominioException("El ID del usuario no pueder ser un Guid vacio.");
        
        if(expedienteId == Guid.Empty)
            throw new DominioException("El ID del expediente asociado no pueder ser un Guid vacio."); 

        if(fechaUltimaModificacion < fechaCreacion)
            throw new DominioException("La fecha de modificacion no puede ser menor a la fecha de creacion.");

        if(fechaCreacion > DateTime.Now)
            throw new DominioException("La fecha de creacion no puede ser mayor a la fecha actual.");

        Id = Guid.NewGuid();
        ExpedienteId = expedienteId;
        UsuarioUltimoCambio = idUsuario;
        Contenido = contenido ?? throw new DominioException("El contenido no puede estar vacio.");
        Etiqueta = EtiquetaTramite.EscritoPresentado;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
    }

    //modificar el contenido del tramite
    public void ModificarContenido(ContenidoTramiteVO nuevoContenido, Guid idUsuario, DateTime fechaCreacion, DateTime fechaUltimaModificacion)
    {
        if(idUsuario == Guid.Empty)
            throw new DominioException("El ID del usuario no puede ser vacío.");

        if(fechaUltimaModificacion < fechaCreacion)
            throw new DominioException("La fecha de modificacion no puede ser menor a la fecha de creacion.");

        if(fechaCreacion > DateTime.Now)
            throw new DominioException("La fecha de creacion no puede ser mayor a la fecha actual.");

        Contenido = nuevoContenido ?? throw new DominioException("El contenido no puede ser nulo.");
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = fechaUltimaModificacion; 
    }

}
