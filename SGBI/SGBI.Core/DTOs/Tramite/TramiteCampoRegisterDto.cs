namespace SGBI.SGBI.Core.DTOs.Tramite;

public class TramiteCampoRegisterDto
{
    public bool DuenoAnterior { get; set; } = false; 
    public bool DuenoActual { get; set; } = false; 
    
    public bool ImponibleAnterior { get; set; } = false;
    public bool ImponibleActual { get; set; } = false;
    
    public bool FolioReal { get; set; } = false;
}