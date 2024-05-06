namespace SGBI.SBGI.Core.Entities;

public class Tramite: EntidadBase
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Codigo { get; set; }

    public string Descripcion {  get; set; }

    public bool estaActivo { get; set; } = true;

    public bool esTramitePorDefecto { get; set; } = false;

    public TramiteCampo? TramiteCampo { get; set; }

    public List<TramiteInformacion?> TramiteInformacion { get; } = new List<TramiteInformacion?>();

}