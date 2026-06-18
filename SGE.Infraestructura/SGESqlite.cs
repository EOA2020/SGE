using Microsoft.EntityFrameworkCore;
using SGE.Aplicacion.Comun;
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

            context.SaveChanges(); 
        }
    }
}
