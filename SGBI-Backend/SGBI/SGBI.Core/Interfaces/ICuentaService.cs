using Microsoft.AspNetCore.Mvc;
using SGBI.SBGI.Core.DTOs;

namespace SGBI.SBGI.Core.Interfaces;

public interface ICuentaService
{
    //Ingreso de usuario y refresco de token
    Task<TokenDto> IngresarUsuarioAsync(UsuarioLoginDto usuarioLoginDto);
    Task<TokenDto> RefrescarToken(TokenDto? tokenDto);

    //Registro de usuario y activación de cuenta
    Task<string> RegistrarUsuarioAsync(UsuarioRegisterDto? usuarioRegisterDto);
    Task<string> ActivarCuentaAsync(CambiarContrasenaDto cambiarContrasenaDto);

    //Cambio de contraseña y envío de correo
    Task<string> EnviarEmailCambioContrasenaAsync(string email);
    Task<string> ResetearContrasenaAsync(CambiarContrasenaDto cambiarContrasenaDto);
    
    
    //Editar usuario
    Task<string> ActualizarUsuarioAsync(UsuarioRegisterDto? usuarioRegisterDto);

    //Obtener todos los usuarios
    Task<List<UsuarioRegisterDto>> ObtenerTodosUsuariosAsync();

    Task<List<String>> ObtenerCedulaUsuariosAsync();
}