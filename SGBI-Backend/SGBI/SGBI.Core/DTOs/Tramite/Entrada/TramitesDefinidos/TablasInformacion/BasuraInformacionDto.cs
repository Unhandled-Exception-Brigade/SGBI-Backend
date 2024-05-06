using SGBI.SBGI.Core.Entities;

namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;

public class BasuraInformacionDto
{

    public int TramiteInformacionId { get; set; }
    public double Tarifa { get; set; }
    public int CantidadTrimestre { get; set; }
    public double MontoAnoActual { get; set; }
    public double MontoAnoAnteriores { get; set; }
    public bool EstaSiendoIncluido { get; set; }

}