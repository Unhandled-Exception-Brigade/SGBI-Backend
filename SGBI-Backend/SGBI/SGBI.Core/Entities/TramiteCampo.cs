namespace SGBI.SBGI.Core.Entities;

public class TramiteCampo : EntidadBase
{
    public int Id { get; set; }
    public bool CodigoDepartamento { get; set; }
    public bool DuenoAnterior { get; set; }
    public bool DuenoActual { get; set; }
    public bool ImponibleAnterior { get; set; }
    public bool ImponibleActual { get; set; }
    public bool FolioReal { get; set; }
    public bool FincaMadre { get; set; }
    public bool Descripcion { get; set; }

    public Tramite Tramite { get; set; }
}