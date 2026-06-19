using System.Data.Common;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;


namespace SGE.Infraestructura;

public class ExpedienteRepository(SGEContext context): IExpedienteRepository
{

    public void AgregarExpediente(Expediente expediente)
    {
/*
    public Guid Id { get; private set; }
    public CaratulaVO Caratula { get; private set; } = null!;
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaUltimaModificacion { get; private set; }
    public Guid UsuarioUltimoCambio { get; private set; }
    public EstadoExpediente Estado { get; private set; }
*/
        //validacion expediente no vacio
        if (expediente == null) throw new RepositorioException("El expediente a agregar no puede estar vacio");

        //Se agrega expediente
        context.Set<Expediente>().Add(expediente);
    }

    public void EliminarExpediente(Guid id)
    {
        //Busca el expediente con Id enviado
        var expediente = context.Set<Expediente>().FirstOrDefault(e => e.Id == id);

        //Validacion de que se haya encontrado el expediente
        if (expediente == null) throw new RepositorioException("No existe expediente con ese Id");

        //Borra el expediente
        context.Set<Expediente>().Remove(expediente);

    }



    public Expediente? ObtenerPorId(Guid id)
    {
        //Busca el expediente con Id enviado
        var expediente = context.Set<Expediente>().FirstOrDefault(e => e.Id == id);

        //Devuelve expediente, si no se encontro devuelve null
        return expediente;

    }

    public List<Expediente> ObtenerTodos()
    {
        //Devuelve la lista de expedientes actual
        return context.Set<Expediente>().ToList();
    }    
}
