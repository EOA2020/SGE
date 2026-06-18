using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class RegistrarUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo uow)
{
    
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        CorreoElectronicoVO correoElectronico;
        try{
            correoElectronico = CorreoElectronicoVO.Parse(request.correoElectronico);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        if(usuarioRepository.ObtenerUsuarioPorCorreo(correoElectronico) != null)
            throw new AplicacionException("EL correo ya se encunetra registrado");

        var usuario = new Usuario(request.nombre,correoElectronico,request.contrasena);

        usuarioRepository.RegistrarUsuario(usuario);

        uow.GuardarCambios();

        return new RegistrarUsuarioResponse();

    }




}