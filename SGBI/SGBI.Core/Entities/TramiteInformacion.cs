namespace SGBI.SGBI.Core.Entities;

public class TramiteInformacion
{
    public int Id { get; set; }


    public string? DuenoAnterior { get; set; }
    public string? DuenoActual { get; set; }

    public string? ImponibleAnterior { get; set; }
    public string? ImponibleActual { get; set; }

    public string? FolioReal { get; set; }


    public Tramite Tramite { get; set; }
    public int TramiteId { get; set; }
    public int TramiteNombre { get; set; }
}