namespace SGBI.SGBI.Core.Entities;

public class Tramite
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Codigo { get; set; }

    public TramiteCampo TramiteCampo { get; set; }
}