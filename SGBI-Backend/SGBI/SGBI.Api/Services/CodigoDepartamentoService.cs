using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.Common.Helpers;
using SGBI.SBGI.Core.DTOs;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Entities;
using SGBI.SGBI.API.Data;
using SGBI.SGBI.API.Models;
using SGBI.SGBI.Core.DTOs.CodigoDepartamento.Entrada;
using SGBI.SGBI.Core.DTOs.CodigoDepartamento.Salida;
using SGBI.SGBI.Core.Entities;
using SGBI.SGBI.Core.Interfaces;

namespace SGBI.SGBI.Api.Services
{
    public class CodigoDepartamentoService : ICodigoDepartamentoService
    {
        
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CodigoDepartamentoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> ActualizarCodigoDepartamento(int id, EditarCodigoDepartamentoDTO editarCodigoDepartamentoDTO)
        {
            if (editarCodigoDepartamentoDTO == null) {
                return "Datos invalidos";
            }

            if (id != null && editarCodigoDepartamentoDTO != null)
            {

                var codigoExiste = await VerificarCodigoExiste(editarCodigoDepartamentoDTO.Codigo);

                if (codigoExiste)
                {
                    var codigo = await _context.CodigoDepartamentos.FirstOrDefaultAsync(x => x.Codigo == editarCodigoDepartamentoDTO.Codigo );
                    if (codigo.Id != id)
                    {
                        return "El codigo ya existe";
                    }
                }

                var descripcionExiste = await VerificarDescripcionExiste(editarCodigoDepartamentoDTO.Descripcion);
                
                if (descripcionExiste)
                {
                    var codigo = await _context.CodigoDepartamentos.FirstOrDefaultAsync(x => x.Descripcion == editarCodigoDepartamentoDTO.Descripcion );
                    if (codigo.Id != id)
                    {
                        return "La descripcion ya existe";
                    }
                }

                var codigoEditado = await _context.CodigoDepartamentos.FirstOrDefaultAsync(x => x.Id == id);

                _mapper.Map(editarCodigoDepartamentoDTO, codigoEditado);

                await _context.SaveChangesAsync();

                return "Codigo de departamento actualizado";
            }

            return "Datos invalidos";
        }

        public async Task<List<ObtenerDescripcionCodigo>> ListarCodigoDescripcionAsync()
        {
            var codigos = await _context.CodigoDepartamentos.ToListAsync();

            var obtenerCodigosDTO = _mapper.Map<List<ObtenerDescripcionCodigo>>(codigos);

            return obtenerCodigosDTO;
        }

        public async Task<List<ObtenerTodosCodigosDTO>> ListarCodigosAsync()
        {
            var codigos = await _context.CodigoDepartamentos.ToListAsync();

            var obtenerCodigosDTO = _mapper.Map<List<ObtenerTodosCodigosDTO>>(codigos);

            return obtenerCodigosDTO;
        }

        public async Task<ObtenerUnCodigoDTO> ObtenerUnCodigoAsync(int id)
        {
            var existeCodigo = await _context.CodigoDepartamentos.FirstOrDefaultAsync(o => o.Id == id);

            if (existeCodigo != null)
            {
                var codigoDepartamento = await _context.CodigoDepartamentos.FirstOrDefaultAsync(o => o.Id == id);

                var codigoDTO = _mapper.Map<CodigoDepartamento, ObtenerUnCodigoDTO>(codigoDepartamento);

                return codigoDTO;
            }

            return null;
        }

        public async Task<string> RegistrarNuevoCodigoAsync(AgregarCodigoDTO agregarCodigoDTO)
        {

            var existeCodigo = await _context.CodigoDepartamentos.FirstOrDefaultAsync(t => t.Codigo == agregarCodigoDTO.Codigo);

            if (existeCodigo != null)
            {
                return "Ya existe un codigo de departamento con este codigo";
            }

            existeCodigo = await _context.CodigoDepartamentos.FirstOrDefaultAsync(t => t.Descripcion == agregarCodigoDTO.Descripcion);

            if (existeCodigo != null)
            {
                return "Ya existe la descripcion en otro codigo de departamento";
            }

            var registrarCodigo = _mapper.Map<AgregarCodigoDTO, CodigoDepartamento>(agregarCodigoDTO);

            await _context.CodigoDepartamentos.AddAsync(registrarCodigo);

            await _context.SaveChangesAsync();

            return "Codigo de departamento registrado";

        }

        private async Task<bool> VerificarCodigoExiste(string codigo)
        {
            if (string.IsNullOrEmpty(codigo)) return false;

            try
            {

                return await _context.CodigoDepartamentos.AnyAsync(x => x.Codigo == codigo);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el codigo: " + ex.Message, ex);
            }
        }

        private async Task<bool> VerificarDescripcionExiste(string descripcion)
        {
            if (string.IsNullOrEmpty(descripcion)) return false;

            try
            {

                return await _context.CodigoDepartamentos.AnyAsync(x => x.Descripcion == descripcion);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar la descripcion: " + ex.Message, ex);
            }
        }
    }
}
