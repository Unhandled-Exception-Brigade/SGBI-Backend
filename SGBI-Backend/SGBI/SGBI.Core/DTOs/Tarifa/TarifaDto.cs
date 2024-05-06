namespace SGBI.SBGI.Core.DTOs.Tarifa;

public class TarifaDto
{
    public string? Descripcion { get; set; }

    public double MontoColones { get; set; }

    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
}