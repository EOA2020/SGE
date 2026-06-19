using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura;

public class SGESqlite
{
    public static void Inicializar(SGEContext context)
    {
        if (context.Database.EnsureCreated())
        {
            // Establecemos la propiedad journal_mode de la base de datos SQLite en DELETE 
            var connection = context.Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }

            List<string> permisos = new List<string>()
            {
                Permiso.ExpedienteAlta.ToString(),
                Permiso.ExpedienteBaja.ToString(),
                Permiso.ExpedienteModificacion.ToString(),
                Permiso.TramiteAlta.ToString(),
                Permiso.TramiteBaja.ToString(),
                Permiso.TramiteModificacion.ToString()
            };

            //administrador semilla
            var administrador = new Usuario(
                "admin",
                CorreoElectronicoVO.Parse("admin@sge.com"),
                HashHelper.GenerarSHA256("admin123"),
                permisos,
                true
            );

            //permisos para el usuario de prueba
            List<string> permisos2 = new List<string>()
            {
                Permiso.ExpedienteAlta.ToString(),
                Permiso.ExpedienteBaja.ToString(),
                Permiso.TramiteAlta.ToString(),
                Permiso.TramiteBaja.ToString()
            };

            var usuario1 = new Usuario(
                "usuarioPrueba1",
                CorreoElectronicoVO.Parse("prueba1@sge.com"),
                HashHelper.GenerarSHA256("prueba1"),
                permisos2
            );

            var usuario2 = new Usuario(
                "usuarioPrueba2",
                CorreoElectronicoVO.Parse("prueba2@sge.com"),
                HashHelper.GenerarSHA256("prueba2"),
                new List<string>()
            );

            context.Set<Usuario>().Add(administrador);
            context.Set<Usuario>().Add(usuario1);
            context.Set<Usuario>().Add(usuario2);

            context.SaveChanges(); 
        }
    }
}
