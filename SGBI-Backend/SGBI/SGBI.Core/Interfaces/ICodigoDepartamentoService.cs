using Microsoft.AspNetCore.Mvc.ModelBinding;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SGBI.Core.DTOs.CodigoDepartamento.Entrada;
using SGBI.SGBI.Core.DTOs.CodigoDepartamento.Salida;

namespace SGBI.SGBI.Core.Interfaces
{
    public interface ICodigoDepartamentoService
    {
        Task<string> RegistrarNuevoCodigoAsync(AgregarCodigoDTO agregarCodigoDTO);

        Task<List<ObtenerTodosCodigosDTO>> ListarCodigosAsync();

        Task<ObtenerUnCodigoDTO> ObtenerUnCodigoAsync(int id);

        Task<string> ActualizarCodigoDepartamento(int id, EditarCodigoDepartamentoDTO editarCodigoDepartamentoDTO);

        Task<List<ObtenerDescripcionCodigo>> ListarCodigoDescripcionAsync();
    }
}
