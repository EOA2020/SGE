using SGE.Aplicacion.Comun;

namespace SGE.Infraestructura;

public class UnidadDeTrabajo: IUnidadDeTrabajo
{
    private readonly SGEContext _context;

    public UnidadDeTrabajo(SGEContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void GuardarCambios()
    {
        _context.SaveChanges();
    }
}
