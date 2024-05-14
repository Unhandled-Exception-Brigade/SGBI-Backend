namespace SGBI.SGBI.Core.DTOs.CodigoDepartamento.Salida
{
    public class ObtenerTodosCodigosDTO
    {
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
