using SGE.Dominio.Comun;

namespace SGE.Dominio.Usuarios;

public class Usuario
{
    public Guid Id {get; private set;}
    public string Nombre {get; private set;} = "";
    public CorreoElectronicoVO CorreoElectronico {get; private set;} = null!;
    public string ContrasenaHash {get; private set;} = "";
    public bool EsAdministrador {get; private set;}
    public List<string> Permisos {get; private set;} = null!;


    public Usuario (string nombre, CorreoElectronicoVO correoElectronico, string contrasenaHash, List<string> permisos)
    {
        if (string.IsNullOrEmpty(nombre))
            throw new DominioException ("El campo nombre no puede estar vacio");  

        if (string.IsNullOrEmpty(contrasenaHash))
        {
            throw new DominioException ("El campo contraseña no puede estar vacio");  
        }

        Id= new Guid();
        Nombre= nombre;
        CorreoElectronico= correoElectronico;
        ContrasenaHash=contrasenaHash;
        EsAdministrador=false;
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
}
