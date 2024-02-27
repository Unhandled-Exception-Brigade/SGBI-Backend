using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Data;
using SGBI.SGBI.Core.DTOs.Tramite;

namespace SGBI.SGBI.API.Services;

public class TramiteService : ITramiteService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public TramiteService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> RegistraNuevoTramiteAsync(TramiteRegisterDto tramiteDto)
    {
        var tramiteExisteNombre = await _context.Tramites.AnyAsync(x => x.Nombre == tramiteDto.Nombre);

        if (tramiteExisteNombre) return "Ya existe un tramite con este nombre";

        var tramiteExisteCodigo = await _context.Tramites.AnyAsync(x => x.Codigo == tramiteDto.Codigo);

        if (tramiteExisteCodigo) return "Ya existe un tramite con este codigo";

        var tramite = _mapper.Map<Tramite>(tramiteDto);

        _context.Add((object)tramite);

        await _context.SaveChangesAsync();

        return "Tramite Creado";
    }
}