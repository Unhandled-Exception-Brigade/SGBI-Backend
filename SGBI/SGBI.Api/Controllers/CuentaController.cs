using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SGBI.SBGI.Core.DTOs;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;

namespace SGBI.SGBI.API.Controllers;

[ApiController]
[Route("api/cuenta")]
public class CuentaController : ControllerBase
{
    private readonly ICuentaService _cuentaService;

    public CuentaController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    [HttpPost("ingresar")] //Endpoint para ingresar usuarios
    public async Task<IActionResult> Ingresar([FromBody] UsuarioLoginDto usuarioLoginDto)
    {
        try
        {
            var usuarioExiste = await _cuentaService.IngresarUsuarioAsync(usuarioLoginDto);

            return Ok(usuarioExiste);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

    [HttpPost("refrescar")] // Endpoint para refrescar el token
    public async Task<IActionResult> Refrescar([FromBody] TokenDto? tokenDto)
    {
        if (tokenDto == null)
            return BadRequest(new { Message = "Datos inválidos" });

        try
        {
            var result = await _cuentaService.RefrescarToken(tokenDto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

    [HttpPost("registrarse")] // Endpoint para registrar usuarios
    public async Task<IActionResult> Registrarse([FromBody] UsuarioRegisterDto? usuarioRegisterDto)
    {
        if (usuarioRegisterDto == null)
            return BadRequest(new { Message = "Datos inválidos" });

        try
        {
            var resultado = await _cuentaService.RegistrarUsuarioAsync(usuarioRegisterDto);

            if (resultado != "Usuario creado")
                return BadRequest(new { Message = resultado });

            return Ok(new
            {
                Message = "Usuario creado, un correo de activación de cuenta ha sido enviado al email del nuevo usuario"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

    [HttpPost("activar-cuenta")]
    public async Task<IActionResult> ActivarCuenta(CambiarContrasenaDto cambiarContrasenaDto)
    {
        try
        {
            var result = await _cuentaService.ActivarCuentaAsync(cambiarContrasenaDto);

            return Ok(new
            {
                StatusCode = 200,
                Message = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }


    [Authorize]
    [HttpPost("actualizar-usuario")]
    public async Task<IActionResult> ActualizarUsuario([FromBody] UsuarioRegisterDto usuarioRegisterDto)
    {
        return null!;
    }

    [HttpPost("resetear-contrasena")]
    public async Task<IActionResult> ResetearContrasena(CambiarContrasenaDto cambiarContrasenaDto)
    {
        try
        {
            var result = await _cuentaService.ResetearContrasenaAsync(cambiarContrasenaDto);

            return Ok(new
            {
                StatusCode = 200,
                Message = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }


    [HttpPost("enviar-email-cambio-contrasena/{email}")]
    public async Task<IActionResult> EnviarEmailCambioContrasena(string email)
    {
        if (string.IsNullOrEmpty(email))
            return BadRequest(new { Message = "Datos inválidos" });

        try
        {
            var result = await _cuentaService.EnviarEmailCambioContrasenaAsync(email);
            return Ok(new
            {
                StatusCode = 200,
                Message = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }

    [Authorize(Roles = "Administrador")]
    [HttpGet]
    public async Task<IActionResult> ObtenerUsuarios() //Endpoint para obtener todos los usuarios
    {
        try
        {
            var usuariosDto = await _cuentaService.ObtenerTodosUsuariosAsync();

            return Ok(usuariosDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Error en el servidor", Error = ex.Message });
        }
    }
}