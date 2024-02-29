namespace SGBI.SGBI.Core.DTOs.Tramite;

public class TramiteInformacionRegisterDto
{
    public int TramiteId { get; set; }

    public string? DuenoAnterior { get; set; }
    public string? DuenoActual { get; set; }

    public string? ImponibleAnterior { get; set; }
    public string? ImponibleActual { get; set; }

    public string? FolioReal { get; set; }
}