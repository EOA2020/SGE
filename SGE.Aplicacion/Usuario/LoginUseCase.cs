using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class LoginUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo uow, ITokenProvider tokenProvider)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {

        //chequemos que el mail sea valido
        CorreoElectronicoVO correoElectronico;
        try{
            correoElectronico = CorreoElectronicoVO.Parse(request.correoElectronico);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //obtenemos su usuario
        Usuario usuario = usuarioRepository.ObtenerUsuarioPorCorreo(correoElectronico);

        //chequeamos que exista
        if(usuario == null)
            throw new AplicacionException("El usuario no existe");
        
        //hasheamos la contraseña que ingresaron
        var contrasenaHash = HashHelper.GenerarSHA256(request.contrasena);

        //la comparamos con la del usuario
        if(usuario.ContrasenaHash != contrasenaHash)
            throw new AplicacionException("La contraseña no coincide");

        //generamos el token perteniente al usuario
        var token = tokenProvider.GenerarToken(usuario);

        //lo retornamos
        return new LoginResponse(token);
    }

   
}
