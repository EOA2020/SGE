using System.Dynamic;
using SGE.Dominio.Comun;
namespace SGE.Dominio.Usuarios;

public class CorreoElectronicoVO
{
    public string Cuenta { get; private init; } = "";
    public string Dominio{ get; private init; }= "";

    public CorreoElectronicoVO (string cuenta, string dominio)
    {
        if (string.IsNullOrWhiteSpace(cuenta) || string.IsNullOrWhiteSpace(dominio))
        {
            throw new DominioException("La cuenta y el dominio son obligatorios en las direcciones de email");
        }

        Cuenta=cuenta;
        Dominio=dominio;
    }

    protected CorreoElectronicoVO(){}

    public static CorreoElectronicoVO Parse (string emailCompleto)
    {
        //Email no puede ser blanco o nulo, y debe contener @
        if (string.IsNullOrWhiteSpace(emailCompleto) || !emailCompleto.Contains('@'))
        {
            throw new DominioException("El formato del email es invalido");
        }

        var partes= emailCompleto.Split('@');
        //Validacion adicional de campos cuenta y dominio
        if (string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
        {
            throw new DominioException("Los campos cuerpo y dominio no puede estar vacios");
        }

        return new CorreoElectronicoVO(partes[0], partes[1]);


    }

    public override string ToString()
    {
        return Cuenta +'@'+ Dominio;
    }


}
