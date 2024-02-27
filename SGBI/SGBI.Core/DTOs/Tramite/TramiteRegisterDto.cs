namespace SGBI.SGBI.Core.DTOs.Tramite;

public class TramiteRegisterDto
{
    public string Nombre { get; set; }

    public string Codigo { get; set; }

    public TramiteCampoRegisterDto TramiteCampo { get; set; }
}