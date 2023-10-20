using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.DTOs.Tarifa;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Data;

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

        int currentYear = DateTime.UtcNow.Year;

        // Verificar si ya existe una tarifa con la misma descripción y año
        var existingTarifa = await _context.Tarifas
            .FirstOrDefaultAsync(t =>
                t.Descripcion == tarifaRegisterDto.Descripcion &&
                t.FechaCreacion != null && t.FechaCreacion.Value.Year == currentYear);

        if (existingTarifa != null)
        {
            // Si existe, actualiza el registro existente en lugar de crear uno nuevo
            existingTarifa.MontoColones = tarifaRegisterDto.MontoColones;
            

            await _context.SaveChangesAsync();

            return "Tarifa Actualizada";
        }
        else
        {
            var tarifaFrontEnd = _mapper.Map<TarifaRegisterDto, Tarifa>(tarifaRegisterDto);
            tarifaFrontEnd.FechaCreacion = DateTime.Now; // Fecha de creación
            // Otros valores que desees establecer al crear una nueva tarifa

            await _context.Tarifas.AddAsync(tarifaFrontEnd);

            await _context.SaveChangesAsync();

            return "Tarifa Creada";
        }
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
            .Where(t => t.Descripcion == "TARIFA EXONERAR")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }

    public async Task<List<TarifaDto>> ListarTarifaMantenimientoAsync()
    {
        var tarifas = await _context.Tarifas
            .Where(t => t.Descripcion == "TARIFA MANTENIMIENTO")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }

    public async Task<List<TarifaDto>> ListarTarifaServiciosAseoAsync()
    {
        var tarifas = await _context.Tarifas
            .Where(t => t.Descripcion == "TARIFA SERVICIOS ASEO")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }

    public async Task<List<TarifaDto>> ListarTarifaServiciosBasuraAsync()
    {
        var tarifas = await _context.Tarifas
            .Where(t => t.Descripcion == "TARIFA SERVICIOS BASURA")
            .ToListAsync();


        var tarifasDto = _mapper.Map<List<TarifaDto>>(tarifas);

        return tarifasDto;
    }
}