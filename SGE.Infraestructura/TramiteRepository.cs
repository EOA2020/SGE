using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;

namespace SGE.Infraestructura;

public class TramiteRepository(SGEContext context) : ITramiteRepository
{
    //Agrega tramite al archivo
    public void AgregarTramite(Tramite tramite)
    {
        //Validacion tramite no vacio
        if (tramite == null) throw new RepositorioException("El tramite no puede estar vacio");

        //Se agrega tramite
        context.Set<Tramite>().Add(tramite);
    }
    
    //elimina tramite del archivo a partir de su id
    public void EliminarTramite(Guid idTramite)
    {
        var tramite= context.Set<Tramite>().FirstOrDefault(t => t.Id == idTramite);

        //Validacion no nulo
        if (tramite==null) throw new RepositorioException("No se ha encontrado el tramite con el id recibido");

        //Se elimina tramite
        context.Set<Tramite>().Remove(tramite);
    }


    //Recorre el archivo buscando los tramites con expedienteID, los devuelve en una lista, si no encuentra devuelve lista vacia
    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        //Filtra los tramites por id de expediente y devuelve una lista con los tramites que coincide el id expediente, o nula si ningun tramite coincide.
        return context.Set<Tramite>().Where(t => t.ExpedienteId.Equals(expedienteId)).ToList();
    }

    //Busca un tramite especifico por su id y devuelve el tramite, sino devuelve null
    public Tramite? ObtenerPorId(Guid idTramite)
    {
        return context.Set<Tramite>().FirstOrDefault(t => t.Id == idTramite);
    }
}
