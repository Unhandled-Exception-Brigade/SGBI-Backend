namespace SGBI.SGBI.Core.DTOs.Reporte;

public class ReporteFiltrosDTO
{
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFinal { get; set; }

    public string? CodigoTramite { get; set; }
    public string? CedulaUsuarioQueRealizoTramite { get; set; }
    public string? PropietarioActual { get; set; }
    public string? FolioReal { get; set; }

    public bool? PDF { get; set; }
    public bool? Excel { get; set;}
}