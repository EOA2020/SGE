using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class RegistrarUsuarioUseCase(
    IUsuarioRepository usuarioRepository, 
    IUnidadDeTrabajo uow,
    ITokenProvider tokenProvider
)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        
        //chequeamos que el mail sea valido
        CorreoElectronicoVO correoElectronico;
        try{
            correoElectronico = CorreoElectronicoVO.Parse(request.CorreoElectronico);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //chequemos que el mail no se encuentre registado
        if(usuarioRepository.ObtenerUsuarioPorCorreo(correoElectronico) != null)
            throw new AplicacionException("EL correo ya se encunetra registrado");

        if(string.IsNullOrWhiteSpace(request.Contrasena))
            throw new AplicacionException("La contrasena no puede estar vacia.");

        var usuario = new Usuario(
            request.Nombre, correoElectronico, 
            HashHelper.GenerarSHA256(request.Contrasena), 
            new List<string>()
        );

        usuarioRepository.RegistrarUsuario(usuario);

        uow.GuardarCambios();

        var token = tokenProvider.GenerarToken(usuario);

        return new RegistrarUsuarioResponse(usuario.Id, token);

    }




}