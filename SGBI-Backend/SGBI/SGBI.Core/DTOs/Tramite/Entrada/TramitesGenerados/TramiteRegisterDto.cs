namespace SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesGenerados;

public class TramiteRegisterDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Codigo { get; set; }

    public bool estaActivo { get; set; } = true;
    public string Descripcion { get; set; }


    public DateTime? FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }

    public TramiteCampoRegisterDto? TramiteCampo { get; set; }
}