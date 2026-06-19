using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;

namespace SGE.Dominio.Usuarios;

public class Usuario
{
    public Guid Id {get; private set;}
    public string Nombre {get; private set;} = "";
    public CorreoElectronicoVO CorreoElectronico {get; private set;} = null!;
    public string ContrasenaHash {get; private set;} = "";
    public bool EsAdministrador {get; private set;}
    public List<string> Permisos {get; private set;} = null!;


    public Usuario (string nombre, CorreoElectronicoVO correoElectronico, string contrasenaHash, List<string> permisos,
    bool esAdministrador = false)
    {
        if (string.IsNullOrEmpty(nombre))
            throw new DominioException ("El campo nombre no puede estar vacio");  

        if (string.IsNullOrEmpty(contrasenaHash))
        {
            throw new DominioException ("El campo contraseña no puede estar vacio");  
        }

        Id= Guid.NewGuid();
        Nombre= nombre;
        CorreoElectronico= correoElectronico;
        ContrasenaHash=contrasenaHash;
        EsAdministrador= esAdministrador;
        Permisos= new List<string> ();

    }

    protected Usuario(){}

    public void ModificarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new DominioException ("El nombre no puede estar vacio");  
        }

        Nombre=nombre;
    }

    public void ModificarCorreoElectronico(CorreoElectronicoVO correoElectronico)
    {
        CorreoElectronico= correoElectronico;
    }


    public void ModificarPermiso(List<string> permisos)
    {
        Permisos= permisos;
    }

    public void ModificarAdministrador(bool esAdministrador)
    {
        EsAdministrador= esAdministrador;
    }

    public void ModificarContrasena(string nuevaContrasenaHash)
    {
        if (string.IsNullOrWhiteSpace(nuevaContrasenaHash))
            throw new DominioException("La contraseña no puede estar vacía");
            
        ContrasenaHash = nuevaContrasenaHash;
    }
}
