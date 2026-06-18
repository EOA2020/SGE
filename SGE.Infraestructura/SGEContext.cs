using Microsoft.EntityFrameworkCore;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura;

public class SGEContext: DbContext
{
    public SGEContext(DbContextOptions<SGEContext> options): base(options)
    {
    }

    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Expediente>().HasKey(e => e.Id);
        modelBuilder.Entity<Tramite>().HasKey(t => t.Id);
        modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
        modelBuilder.Entity<Expediente>().ComplexProperty(e => e.Caratula);
        modelBuilder.Entity<Tramite>().ComplexProperty(t => t.Contenido);
        modelBuilder.Entity<Usuario>().ComplexProperty(u => u.CorreoElectronico);
    }

}
