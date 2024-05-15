namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

public class ExoneracionInformacionDto
{
    public int TramiteInformacionId { get; set; }
    public double MontoExonerar { get; set; }
    public double Excedente { get; set; } = 0;
    public int CantidadTrimestre { get; set; }
}