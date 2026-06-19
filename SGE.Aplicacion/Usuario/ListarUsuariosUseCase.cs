using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

public class ListarUsuariosUseCase(IUsuarioRepository usuarioRepository)
{
    public ListarUsuariosResponse Ejecutar(ListarUsuariosRequest request)
    {

        //chequeamos que el mail sea valido
        CorreoElectronicoVO correoSolicitante;
        try
        {
            correoSolicitante = CorreoElectronicoVO.Parse(request.correoElectronico);
        }
        catch(DominioException)
        {
            throw new AplicacionException("El formato del email es inválido");
        }

        //obtenemos su usuario
        var usuarioSolicitante = usuarioRepository.ObtenerUsuarioPorCorreo(correoSolicitante);
        
        //chequeamos que exista
        if (usuarioSolicitante == null)
            throw new EntidadNoEncontradaException("El usuario solicitante no existe");

        //chequeamos que sea administrador
        if (!usuarioSolicitante.EsAdministrador)
            throw new AutorizacionException("El usuario no es administrador");
            
        //obtenemos todos los usuarios
        var usuarios = usuarioRepository.ListarUsuarios();

        //cremos la lista de usuarios
        var dtos = new List<UsuarioDTO>();

        //la recorremos
        foreach(var u in usuarios)
        {
            //transformamos el Usuario en UsuarioDTO
            var dto = new UsuarioDTO(
                u.Id,
                u.Nombre,
                u.CorreoElectronico,
                u.ContrasenaHash,
                u.EsAdministrador,
                u.Permisos
            );
            dtos.Add(dto);
        }

        //retornamos la respuesta
        return new ListarUsuariosResponse(dtos);
    }
}