namespace SGBI.SGBI.Core.DTOs.Tramite;

public class ObtenerTramitesNombreIdDTO
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Codigo { get; set; }

    public bool esTramitePorDefecto { get; set; }
}