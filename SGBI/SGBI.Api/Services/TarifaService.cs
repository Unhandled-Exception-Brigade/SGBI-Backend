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