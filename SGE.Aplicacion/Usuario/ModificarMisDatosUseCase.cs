using SGE.Aplicacion.Comun;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;

public class ModificarMisDatosUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo uow)
{
    public ModificarMisDatosResponse Ejecutar(ModificarMisDatosRequest request, Guid IdAdmin)
    {
        //chequeamos el que el token no este vacio
        if (IdAdmin == Guid.Empty)
            throw new AplicacionException("El token no contiene un ID de usuario válido.");

        //buscamos el usuario correspondiente al token
        var usuario = usuarioRepository.ObtenerUsuarioPorId(IdAdmin);

        //chequeamos que exista
        if (usuario == null)
            throw new EntidadNoEncontradaException("El usuario no existe.");

        //modificamos el dato correpondiente
        if (request.NuevoNombre != null)
        {
            usuario.ModificarNombre(request.NuevoNombre);
        }

        if (request.NuevoCorreo != null)
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

        if(request.NuevaContrasenaHash != null)
        {
            usuario.ModificarContrasena(HashHelper.GenerarSHA256(request.NuevaContrasenaHash));
        }

        
        uow.GuardarCambios();

        return new ModificarMisDatosResponse(IdAdmin);
    }
}