using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class LoginUseCase(
    IUsuarioRepository usuarioRepository, 
    ITokenProvider tokenProvider
)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {

        //chequemos que el mail sea valido
        CorreoElectronicoVO correoElectronico;
        try{
            correoElectronico = CorreoElectronicoVO.Parse(request.CorreoElectronico);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //obtenemos su usuario
        var usuario = usuarioRepository.ObtenerUsuarioPorCorreo(correoElectronico);

        //chequeamos que exista
        if(usuario == null)
            throw new EntidadNoEncontradaException("El usuario no existe");
        
        //hasheamos la contraseña que ingresaron
        var contrasenaHash = HashHelper.GenerarSHA256(request.Contrasena);

        //la comparamos con la del usuario
        if(usuario.ContrasenaHash != contrasenaHash)
            throw new AutorizacionException("La contraseña es Incorrecta.");

        //generamos el token perteniente al usuario
        var token = tokenProvider.GenerarToken(usuario);

        //lo retornamos
        return new LoginResponse(token);
    }

   
}
