using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Data;
using SGBI.SGBI.Core.DTOs.Tarifa;
using SGBI.SGBI.Core.Entities;

namespace SGBI.SGBI.API.Services;

public class TarifaService : ITarifaService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TarifaService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> RegistrarNuevaTarifaAsync(TarifaRegisterDto tarifaRegisterDto)
    {
        tarifaRegisterDto.Descripcion = tarifaRegisterDto.Descripcion!.ToUpper();

        if (tarifaRegisterDto.Descripcion == "TARIFA MONTO MAXIMO A EXONERAR")
        {
            var currentYear = DateTime.Now.Year;

            // Verificar si ya existe una tarifa para el año actual con el mismo monto
            var existingTarifa = await _context.Tarifas
                .FirstOrDefaultAsync(t => t.FechaModificacion!.Value.Year == currentYear
                                          && t.MontoColones == tarifaRegisterDto.MontoColones);

            if (existingTarifa != null) return "Ya existe una tarifa para este año con el mismo monto";
        }

        var tarifaFrontEnd = _mapper.Map<TarifaRegisterDto, Tarifa>(tarifaRegisterDto);

        await _context.Tarifas.AddAsync(tarifaFrontEnd);

        await _context.SaveChangesAsync();

        return "Tarifa Creada";
    }


    public async Task<List<TarifaDto>> ListarTarifasAsync()
    {
        var tarifas = await _context.Tarifas.ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }


    public async Task<List<TarifaDto>> ListarTarifaExonerarAsync()
    {
        var tarifas = await _context.Tarifas
            .Where(t => t.Descripcion == "TARIFA MONTO MAXIMO A EXONERAR")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }

    public async Task<List<TarifaDto>> ListarTarifaMantenimientoAsync()
    {
        var tarifas = await _context.Tarifas
            .Where(t => t.Descripcion == "TARIFA MANTENIMIENTO DE PARQUES Y OBRAS DE ORNATO")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }

    public async Task<List<TarifaDto>> ListarTarifaServiciosAseoAsync()
    {
        var tarifas = await _context.Tarifas
            .Where(t => t.Descripcion == "TARIFA SERVICIOS ASEO DE VIAS Y SITIOS PUBLICOS")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }

    public async Task<List<TarifaDto>> ListarTarifaServiciosBasuraAsync()
    {
        var tarifas = await _context.Tarifas
            .Where(t => t.Descripcion == "TARIFA SERVICIOS DE RECOLECCION DE BASURA")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }
}