using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SGBI.SGBI.Core.Entities;

public class Usuario : IdentityUser
{
    [Required] public string? Nombre { get; set; }

    [Required] public string? PrimerApellido { get; set; }

    [Required] public string? SegundoApellido { get; set; }

    [Required] public bool Activo { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenFechaExpiracion { get; set; }

    public string? CambiarContrasenaToken { get; set; }

    public DateTime CambiarContrasenaFechaExpiracion { get; set; }
}