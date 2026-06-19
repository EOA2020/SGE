using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class RegistrarUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo uow)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        
        //chequeamos que el mail sea valido
        CorreoElectronicoVO correoElectronico;
        try{
            correoElectronico = CorreoElectronicoVO.Parse(request.correoElectronico);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //chequemos que el mail no se encuentre registado
        if(usuarioRepository.ObtenerUsuarioPorCorreo(correoElectronico) != null)
            throw new AplicacionException("EL correo ya se encunetra registrado");

        var usuario = new Usuario(request.nombre,correoElectronico,request.contrasena, new List<string>());

        usuarioRepository.RegistrarUsuario(usuario);

        uow.GuardarCambios();

        return new RegistrarUsuarioResponse();

    }




}