namespace SGBI.SBGI.Core.Entities;

public abstract class EntidadBase
{
    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }


    public string? UsuarioCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
}