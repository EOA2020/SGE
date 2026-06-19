

using SGE.Aplicacion.Comun;

public class ObtenerUsuarioPorIdUseCase(IUsuarioRepository usuarioRepository)
{
    public ObtenerUsuarioPorIdResponse Ejecutar(ObtenerUsuarioPorIdRequest request)
    {
        //chequeamos que el id del usuario no este vacio
        if (request.IdUsuario == Guid.Empty)
            throw new AplicacionException("El ID del usuario no puede estar vacío.");

        //obtenemos su usuario
        var usuario = usuarioRepository.ObtenerUsuarioPorId(request.IdUsuario);

        // chequeamos que exista
        if (usuario == null)
            throw new EntidadNoEncontradaException("El usuario no existe.");

        // recreamos el usuario obtenido
        var dto = new UsuarioDTO(
            usuario.Id,
            usuario.Nombre,
            usuario.CorreoElectronico, 
            usuario.ContrasenaHash,
            usuario.EsAdministrador,
            usuario.Permisos
        );

        //retornarnamos
        return new ObtenerUsuarioPorIdResponse(dto);
    }
}
