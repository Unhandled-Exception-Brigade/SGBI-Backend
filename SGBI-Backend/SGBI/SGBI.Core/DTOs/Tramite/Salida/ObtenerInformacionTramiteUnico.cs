namespace SGBI.SGBI.Core.DTOs.Tramite.Salida
{
    public class ObtenerInformacionTramiteUnico
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool estaActivo { get; set; } = true;

        public bool esTramitePorDefecto { get; set; } = false;
    }
}
