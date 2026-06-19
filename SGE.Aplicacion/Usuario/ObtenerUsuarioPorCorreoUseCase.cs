using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class ObtenerUsuarioPorCorreo(IUsuarioRepository usuarioRepository)
{
    public ObtenerUsuarioPorIdResponse Ejecutar(ObtenerUsuarioPorCorreoRequest request)
    {
        //chequemaos que el correo sea valido
        CorreoElectronicoVO correoElectronico;
        try{
            correoElectronico = CorreoElectronicoVO.Parse(request.correoElectronico);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es invalido");
        }

        //obtenemos el usuario
        Usuario usuario = usuarioRepository.ObtenerUsuarioPorCorreo(correoElectronico);

        //chequeamos que exista
        if (usuario == null)
            throw new EntidadNoEncontradaException("El usuario no existe");

        //recreamos el usuario obtenido 
        var dto = new UsuarioDTO(
            usuario.Id,
            usuario.Nombre,
            usuario.CorreoElectronico, 
            usuario.ContrasenaHash,
            usuario.EsAdministrador,
            usuario.Permisos
        );

        //lo retornamos
        return new ObtenerUsuarioPorIdResponse(dto);
    }
}