using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SGBI.SBGI.Core.Common.Helpers;
using SGBI.SBGI.Core.DTOs;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Models;

namespace SGBI.SGBI.API.Services;

public class CuentaService : ICuentaService
{
    private readonly UserManager<Usuario> _userManager;


    private readonly ICorreoService _correoService;


    private readonly IConfiguration _configuration;

    private readonly IMapper _mapper;


    public CuentaService(UserManager<Usuario> userManager, ICorreoService correoService,
        IConfiguration configuration, IMapper mapper)

    {
        _userManager = userManager;

        _correoService = correoService;

        _configuration = configuration;

        _mapper = mapper;
    }

    public async Task<TokenDto> IngresarUsuarioAsync(UsuarioLoginDto usuarioLoginDto)
    {
        if (usuarioLoginDto == null)
            throw new ArgumentNullException(nameof(usuarioLoginDto), "Datos vacíos");

        try
        {
            var usuario = await _userManager.FindByNameAsync(usuarioLoginDto.Cedula!);

            if (usuario == null)
                throw new UnauthorizedAccessException("Usuario no existe");

            if (!usuario.Activo)
                throw new UnauthorizedAccessException("Usuario marcado como inactivo");


            //Trae la constrasena hash desde la base de datos

            var passwordHash = usuario.PasswordHash;

            if (!PasswordHasher.VerifyPassword(usuarioLoginDto.Contrasena!, passwordHash!))
                throw new UnauthorizedAccessException("Contraseña incorrecta");


            var userRoles = await _userManager.GetRolesAsync(usuario);

            // Genera claims para el token
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.UserName!),
                new Claim(ClaimTypes.Actor, usuario.Nombre!),
            };

            // Agrega los roles del usuario como claims
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            foreach (var claim in authClaims)
            {
                await _userManager.AddClaimAsync(usuario, claim);
            }
          

            var token = GenerateToken.CreateToken(_configuration, authClaims);

            var refreshToken = GenerateToken.GenerateRefreshToken();

            _ = int.TryParse(
                _configuration["JWT:RefreshTokenValidityInDays"],
                out int refreshTokenValidityInDays);

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenFechaExpiracion = DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(usuario);

            // Regresa los tokens
            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error al ingresar usuario: " + ex.Message, ex);
        }
    }


    public async Task<TokenDto> RefrescarToken(TokenDto? tokenDto)
    {
        if (tokenDto == null)
            throw new ArgumentNullException(nameof(tokenDto), "Petición del cliente inválida");

        try
        {
            var accessToken = tokenDto.AccessToken;
            var refreshToken = tokenDto.RefreshToken;

            var principal = GenerateToken.GetPrincipalFromExpiredToken(_configuration, accessToken);
            if (principal == null)
            {
                throw new UnauthorizedAccessException("Token inválido");
            }

            string username = principal.Identity!.Name!;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenFechaExpiracion <= DateTime.Now)
            {
                throw new UnauthorizedAccessException("Token de refresco inválido");
            }

            var newAccessToken = GenerateToken.CreateToken(_configuration, principal.Claims.ToList());
            var newRefreshToken = GenerateToken.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);


            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException( ex.Message, ex);
        }
    }


    public async Task<string> RegistrarUsuarioAsync(UsuarioRegisterDto? usuarioRegisterDto)
    {
        if (usuarioRegisterDto == null)
            throw new ArgumentNullException(nameof(usuarioRegisterDto), "Datos vacíos");

        try
        {
            // Verifica si el usuario ya existe por su cédula
            var existingUser = await _userManager.FindByNameAsync(usuarioRegisterDto.Cedula!);

            if (existingUser != null)
            {
                if (existingUser.Activo)
                {
                    return "Usuario ya existe y está marcado como activo.";
                }
                else
                {
                    return "Usuario ya existe y está marcado como Inactivo.";
                }
            }

            // Verifica si el correo ya existe
            var existingEmailUser = await _userManager.FindByEmailAsync(usuarioRegisterDto.Email!);

            if (existingEmailUser != null)
            {
                return "Correo ya existe.";
            }

            var newUser = new Usuario
            {
                UserName = usuarioRegisterDto.Cedula,
                Nombre = usuarioRegisterDto.Nombre,
                PrimerApellido = usuarioRegisterDto.PrimerApellido,
                SegundoApellido = usuarioRegisterDto.SegundoApellido,
                Email = usuarioRegisterDto.Email,
                Activo = false // Marcar como inactivo por defecto
            };

            var result = await _userManager.CreateAsync(newUser);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Usuario");


                await EnviarEmailActivacionAsync(usuarioRegisterDto.Email!);

                return "Usuario creado";
            }
            else
            {
                return "Error al registrar el usuario. Verifique los datos.";
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al registrar usuario: " + ex.Message, ex);
        }
    }


    private async Task EnviarEmailActivacionAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email), "Petición del cliente inválida");

        try
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new UnauthorizedAccessException("Usuario no existe");


            //Como estoy utilizando Identity tiene una validez de 24 horas
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            user.CambiarContrasenaToken = token;
            user.CambiarContrasenaFechaExpiracion =
                DateTime.UtcNow.AddDays(1); //Aca le puse de un dia la activacion de la cuenta

            await _userManager.UpdateAsync(user);
            var correoModel = new CorreoModel(email, "Active su cuenta - SGBI",
                EmailBody.ActivateAccount(email, token));


            _correoService.EnviarCorreo(correoModel);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException( ex.Message, ex);
        }
    }

    public async Task<string> ActivarCuentaAsync(CambiarContrasenaDto cambiarContrasenaDto)
    {
        if (cambiarContrasenaDto == null)
            throw new ArgumentNullException(nameof(cambiarContrasenaDto), "Petición del cliente inválida");

        try
        {
            var nuevoToken = cambiarContrasenaDto.EmailToken!.Replace(" ", "+");

            var usuario = await _userManager.FindByEmailAsync(cambiarContrasenaDto.Email!);

            if (usuario == null)
                throw new UnauthorizedAccessException("Usuario no existe");


            var tokenCode = usuario.CambiarContrasenaToken;
            var tokenExpiration = usuario.CambiarContrasenaFechaExpiracion;

            if (tokenCode != nuevoToken || tokenExpiration < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Link inválido");


            usuario.PasswordHash = PasswordHasher.HashPassword(cambiarContrasenaDto.NuevaContrasena!);


            var email = new CorreoModel(usuario.Email!, "Su cuenta ha sido activada - SGBI",
                EmailBody.ActivateAccountSucess(cambiarContrasenaDto.Email!));
            _correoService.EnviarCorreo(email);

            usuario.CambiarContrasenaToken = null;

            usuario.Activo = true;


            usuario.EmailConfirmed = true;
            await _userManager.UpdateAsync(usuario);

            return "El usuario ha sido activado exitosamente";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
    }

    public async Task<string> EnviarEmailCambioContrasenaAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email), "Petición del cliente inválida");

        try
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario is null)
                throw new UnauthorizedAccessException("Usuario no existe");

            if (!usuario.Activo)
                throw new UnauthorizedAccessException("Usuario marcado como Inactivo");

            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);

            usuario.CambiarContrasenaToken = emailToken;
            usuario.CambiarContrasenaFechaExpiracion = DateTime.UtcNow.AddMinutes(15);

            var from = _configuration["EmailSettings:From"];
            var Email = new CorreoModel(email, "Cambiar su contraseña - SGBI",
                EmailBody.ChangePasswordRequest(email, emailToken));

            _correoService.EnviarCorreo(Email);

            await _userManager.UpdateAsync(usuario);

            return "Un email ha sido enviado a su dirección de correo exitosamente";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
    }

    public async Task<string> ResetearContrasenaAsync(CambiarContrasenaDto cambiarContrasenaDto)
    {
        if (cambiarContrasenaDto == null)
            throw new ArgumentNullException(nameof(cambiarContrasenaDto), "Petición del cliente inválida");

        try
        {
            var nuevoToken = cambiarContrasenaDto.EmailToken!.Replace(" ", "+");

            var usuario = await _userManager.FindByEmailAsync(cambiarContrasenaDto.Email!);

            if (usuario == null)
                throw new UnauthorizedAccessException("Usuario no existe");


            var tokenCode = usuario.CambiarContrasenaToken;
            var tokenExpiration = usuario.CambiarContrasenaFechaExpiracion;

            if (tokenCode != nuevoToken || tokenExpiration < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Link inválido");

            if (PasswordHasher.VerifyPassword(cambiarContrasenaDto.NuevaContrasena!, usuario.PasswordHash!))
            {
                throw new UnauthorizedAccessException("La nueva contraseña no puede ser identica a la anterior");
            }


            usuario.PasswordHash = PasswordHasher.HashPassword(cambiarContrasenaDto.NuevaContrasena!);


            var Email = new CorreoModel(usuario.Email, "Confirmación cambio contraseña - SGBI",
                EmailBody.PasswordChangeConfirm(cambiarContrasenaDto.Email));
            _correoService.EnviarCorreo(Email);

            usuario.CambiarContrasenaToken = null;


            await _userManager.UpdateAsync(usuario);


            return "La contraseña ha sido cambiada exitosamente";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException( ex.Message, ex);
        }
    }
    
    
     public async Task<string> ActualizarUsuarioAsync(UsuarioRegisterDto? usuarioRegisterDto)
    {

        if (string.IsNullOrWhiteSpace(usuarioRegisterDto!.Nombre))
            throw new ArgumentNullException(nameof(usuarioRegisterDto.Nombre), "Nombre vacío");

        if (string.IsNullOrWhiteSpace(usuarioRegisterDto.PrimerApellido))
            throw new ArgumentNullException(nameof(usuarioRegisterDto.PrimerApellido), "Apellido vacío");

        if (string.IsNullOrWhiteSpace(usuarioRegisterDto.SegundoApellido))
            throw new ArgumentNullException(nameof(usuarioRegisterDto.SegundoApellido), "Apellido vacío");

        if (string.IsNullOrWhiteSpace(usuarioRegisterDto.Email))
            throw new ArgumentNullException(nameof(usuarioRegisterDto.Email), "Correo vacío");

        try
        {
            var enviarCorreoPrimeraVez = false;

            var usuario = await _userManager.FindByNameAsync(usuarioRegisterDto.Cedula!);

            if (usuario.Email != usuarioRegisterDto.Email)
            {
                if (await VerificarCorreoExisteAsync(usuarioRegisterDto.Email))
                {
                    throw new UnauthorizedAccessException("Correo ya perteneciente a otro usuario");
                }
                if (!usuario.Activo)
                {
                    enviarCorreoPrimeraVez = true;
                }else
                {
                    var from = _configuration["EmailSettings:From"];
                    var Email = new CorreoModel(usuarioRegisterDto.Email, "Cambio de correo electrónico - SGBI", EmailBody.ChangeEmailSucess(usuarioRegisterDto.Email));

                    _correoService.EnviarCorreo(Email);
                }
            }

            usuario.Nombre = usuarioRegisterDto.Nombre;
            usuario.PrimerApellido = usuarioRegisterDto.PrimerApellido;
            usuario.SegundoApellido = usuarioRegisterDto.SegundoApellido;
            usuario.Email = usuarioRegisterDto.Email;
            usuario.Activo = (bool)usuarioRegisterDto.Activo!;
            
            
            // Remueve al usuario de todos los roles actuales
            var rolesActuales = await _userManager.GetRolesAsync(usuario);
            await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);

            
            await _userManager.AddToRolesAsync(usuario, usuarioRegisterDto.Rol!);
            
            

            await _userManager.UpdateAsync(usuario);
            
            if (enviarCorreoPrimeraVez)
            {

                await EnviarEmailActivacionAsync(usuarioRegisterDto.Email);
            }

            return "Usuario Actualizado";
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Error al actualizar el usuario: " + ex.Message, ex);
        }
    }


    public async Task<List<UsuarioRegisterDto>> ObtenerTodosUsuariosAsync()
    {
        try
        {
            var users = await _userManager.Users.ToListAsync();

            var usuariosConRolesDto = new List<UsuarioRegisterDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var usuarioConRolDto = new UsuarioRegisterDto
                {
                    Cedula = user.UserName,
                    Nombre = user.Nombre,
                    PrimerApellido = user.PrimerApellido,
                    SegundoApellido = user.SegundoApellido,
                    Email = user.Email,
                    Rol = roles.ToList(),
                    Activo = user.Activo
                };
                usuariosConRolesDto.Add(usuarioConRolDto);
            }

            return usuariosConRolesDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener usuarios con roles: " + ex.Message, ex);
        }
    }

    public async Task<List<string>> ObtenerCedulaUsuariosAsync()
    {
        try
        {
            var usuarios = await _userManager.Users.ToListAsync();
            
            // Creamos una lista para almacenar las cédulas
            List<string> cedulas = new List<string>();

            // Iteramos sobre cada usuario y agregamos su cédula a la lista
            foreach (var usuario in usuarios)
            {
                cedulas.Add(usuario.UserName);
            }

            return cedulas;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener cedulas: " + ex.Message, ex);
        }
    }


    private async Task<bool> VerificarCorreoExisteAsync(string email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        try
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al verificar correo: " + ex.Message, ex);
        }
    }
}