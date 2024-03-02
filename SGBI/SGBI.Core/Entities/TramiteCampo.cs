namespace SGBI.SGBI.Core.Entities;

public class TramiteCampo
{
    public int Id { get; set; }

    public bool DuenoAnterior { get; set; }
    public bool DuenoActual { get; set; }

    public bool ImponibleAnterior { get; set; }
    public bool ImponibleActual { get; set; }

    public bool FolioReal { get; set; }
}