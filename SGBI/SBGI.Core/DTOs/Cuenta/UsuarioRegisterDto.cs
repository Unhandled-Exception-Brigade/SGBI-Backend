namespace SGBI.SBGI.Core.DTOs;

public class UsuarioRegisterDto
{
    public string? Cedula { get; set; }

    public string? Nombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }
    public string? Email { get; set; }

    public IList<string>? Rol { get; set; }

    public bool? Activo { get; set; }
}