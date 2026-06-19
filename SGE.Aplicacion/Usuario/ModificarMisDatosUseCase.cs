using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public class ModificarMisDatosUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo uow)
{
    public ModificarMisDatosResponse Ejecutar(ModificarMisDatosRequest request)
    {
        //chequeamos el que el token no este vacio
        if (request.IdUsuarioDesdeToken == Guid.Empty)
            throw new AplicacionException("El token no contiene un ID de usuario válido.");

        //buscamos el usuario correspondiente al token
        var usuario = usuarioRepository.ObtenerUsuarioPorId(request.IdUsuarioDesdeToken);

        //chequeamos que exista
        if (usuario == null)
            throw new EntidadNoEncontradaException("El usuario no existe.");

        //modificamos el dato correspondiente
        if (!string.IsNullOrWhiteSpace(request.NuevoNombre))
        {
            usuario.ModificarNombre(request.NuevoNombre);
        }

        if (!string.IsNullOrWhiteSpace(request.NuevoCorreo))
        {
            try
            {
                var nuevoCorreoVO = CorreoElectronicoVO.Parse(request.NuevoCorreo);
                usuario.ModificarCorreoElectronico(nuevoCorreoVO);
            }
            catch (DominioException)
            {
                throw new AplicacionException("El formato del nuevo correo es inválido.");
            }
        }

        if (!string.IsNullOrWhiteSpace(request.NuevaContrasenaHash))
        {
            usuario.ModificarContrasena(request.NuevaContrasenaHash);
        }

        
        uow.GuardarCambios();

        return new ModificarMisDatosResponse(request.IdUsuarioDesdeToken);
    }
}