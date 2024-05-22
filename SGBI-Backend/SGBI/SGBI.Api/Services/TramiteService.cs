using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGBI.SBGI.Core.Entities;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Data;
using SGBI.SGBI.Core.DTOs.Tramite;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.Registro;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesDefinidos.TablasInformacion;
using SGBI.SGBI.Core.DTOs.Tramite.Entrada.TramitesGenerados;
using SGBI.SGBI.Core.DTOs.Tramite.Salida;

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

    public async Task<ActionResult<ObtenerCamposTramiteDTO>> ObtenerCamposDelTramiteAsync(
    ObtenerTramitesDTO obtenerTramitesDTO)
    {
        var tramite = await _context.Tramites.Include(x => x.TramiteCampo).FirstOrDefaultAsync(x => x.Id == obtenerTramitesDTO.IdTramiteCampos);

        if (tramite != null)
        {
            if (tramite.TramiteCampo != null)
            {
                var tramiteCampo = await _context.TramiteCampos.FirstOrDefaultAsync(x => x.Id == tramite.TramiteCampo.Id);

                var campos = _mapper.Map<ObtenerCamposTramiteDTO>(tramiteCampo);

                return campos;
            }
        }
        

        return null;
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

    public async Task<string> UsarTramiteAsync(TramiteInformacionRegisterDto tramiteInformacionRegisterDto)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == tramiteInformacionRegisterDto.TramiteId);

        if (tramite == null) return "Tramite inexistente";


        if (tramite.estaActivo == false) return "El tramite se encuentra inactivo";

        if (tramite != null)
        {
            int currentYear = DateTime.UtcNow.Year;

            int counter = await _context.TramitesInformacion
                .Where(x => x.FechaCreacion.HasValue && x.FechaCreacion.Value.Year == currentYear)
                .CountAsync();

            counter++;

            tramiteInformacionRegisterDto.NumeroDocumento = "MAT-IBI-DOC-" + counter + "-" + currentYear;

            var tramiteInformacion = _mapper.Map<TramiteInformacion>(tramiteInformacionRegisterDto);

            _context.Add(tramiteInformacion);

            await _context.SaveChangesAsync();

            return tramiteInformacionRegisterDto.NumeroDocumento;
        }

        return "Tramite inexistente";
    }

    public async Task<string> RegistrarExoneracion(ExoneracionRegistroDto exoneracionRegistroDto)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == exoneracionRegistroDto.TramiteId);

        int TramitePK = 0;

        if (tramite != null)
        {
            int currentYear = DateTime.UtcNow.Year;

            int counter = await _context.TramitesInformacion
                .Where(x => x.FechaCreacion.HasValue && x.FechaCreacion.Value.Year == currentYear)
                .CountAsync();

            counter++;

            exoneracionRegistroDto.NumeroDocumento = "MAT-IBI-DOC-" + counter + "-" + currentYear;

            var tramiteInformacion = _mapper.Map<TramiteInformacion>(exoneracionRegistroDto);
            _context.Add(tramiteInformacion);
            int FilaCreada = await _context.SaveChangesAsync();


            if (FilaCreada > 0)
            {
                TramitePK = tramiteInformacion.Id;
            }
            
            if (exoneracionRegistroDto.ImponibleActual != null)
            {
                exoneracionRegistroDto.ExoneracionInformacionDTO.TramiteInformacionId = TramitePK;
            }

            var ExoneracionInformacion = _mapper.Map<ExoneracionInformacion>(exoneracionRegistroDto.ExoneracionInformacionDTO);
            _context.Add(ExoneracionInformacion);

            await _context.SaveChangesAsync();

            return exoneracionRegistroDto.NumeroDocumento;
        }
        else
        {
            return "Tramite inexistente";
        }
    }
    public async Task<string> RegistrarBasura(ServicioBasuraRegistroDto servicioBasuraRegistroDto)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == servicioBasuraRegistroDto.TramiteId);

        int TramitePK = 0;

        if (tramite != null)
        {
            int currentYear = DateTime.UtcNow.Year;

            int counter = await _context.TramitesInformacion
                .Where(x => x.FechaCreacion.HasValue && x.FechaCreacion.Value.Year == currentYear)
                .CountAsync();

            counter++;

            servicioBasuraRegistroDto.NumeroDocumento = "MAT-IBI-DOC-" + counter + "-" + currentYear;

            var tramiteInformacion = _mapper.Map<TramiteInformacion>(servicioBasuraRegistroDto);
            _context.Add(tramiteInformacion);
            int FilaCreada = await _context.SaveChangesAsync();

            if (FilaCreada > 0)
            {
                TramitePK = tramiteInformacion.Id;
            }

            if (servicioBasuraRegistroDto.BasuraInformacionDTO != null)
            {

                servicioBasuraRegistroDto.BasuraInformacionDTO.TramiteInformacionId = TramitePK;

                //CalcularDatosBasuraInformacion(servicioBasuraRegistroDto);

                var InclusionBasuraInformacion = _mapper.Map<BasuraInformacion>(servicioBasuraRegistroDto.BasuraInformacionDTO);
                _context.Add(InclusionBasuraInformacion);
                Console.WriteLine(InclusionBasuraInformacion.ToString());
            }

            await _context.SaveChangesAsync();

            return servicioBasuraRegistroDto.NumeroDocumento;
        }
        else
        {
            return "Tramite inexistente";   
        }
    }

    public async  Task<string> RegistrarParquesObras(ServicioParquesOrnatoRegistroDto servicioParquesOrnatoRegistroDto)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == servicioParquesOrnatoRegistroDto.TramiteId);

        int TramitePK = 0;
        if (tramite != null)
        {
            int currentYear = DateTime.UtcNow.Year;

            int counter = await _context.TramitesInformacion
                .Where(x => x.FechaCreacion.HasValue && x.FechaCreacion.Value.Year == currentYear)
                .CountAsync();

            counter++;

            servicioParquesOrnatoRegistroDto.NumeroDocumento = "MAT-IBI-DOC-" + counter + "-" + currentYear;

            var tramiteInformacion = _mapper.Map<TramiteInformacion>(servicioParquesOrnatoRegistroDto);
            _context.Add(tramiteInformacion);
            int FilaCreada = await _context.SaveChangesAsync();

            if (FilaCreada > 0)
            {
                TramitePK = tramiteInformacion.Id;
            }

            if (servicioParquesOrnatoRegistroDto.ParqueInformacionDTO != null)
            {
                servicioParquesOrnatoRegistroDto.ParqueInformacionDTO.TramiteInformacionId = TramitePK;

                var ParqueInformacion = _mapper.Map<ParqueInformacion>(servicioParquesOrnatoRegistroDto.ParqueInformacionDTO);
                _context.Add(ParqueInformacion);
            }

            await _context.SaveChangesAsync();

            return servicioParquesOrnatoRegistroDto.NumeroDocumento;
        }
        else
        {
            return "Tramite inexistente";
        }
    }

    public async Task<string> RegistrarAseoVias(ServicioAseoViasRegistroDto servicioAseoViasRegistroDto)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == servicioAseoViasRegistroDto.TramiteId);

        int TramitePK = 0;
        if (tramite != null)
        {
            int currentYear = DateTime.UtcNow.Year;

            int counter = await _context.TramitesInformacion
                .Where(x => x.FechaCreacion.HasValue && x.FechaCreacion.Value.Year == currentYear)
                .CountAsync();

            counter++;

            servicioAseoViasRegistroDto.NumeroDocumento = "MAT-IBI-DOC-" + counter + "-" + currentYear;

            var tramiteInformacion = _mapper.Map<TramiteInformacion>(servicioAseoViasRegistroDto);
            _context.Add(tramiteInformacion);
            int FilaCreada = await _context.SaveChangesAsync();

            if (FilaCreada > 0)
            {
                TramitePK = tramiteInformacion.Id;
            }

            if (servicioAseoViasRegistroDto.AseoInformacionDTO != null)
            {
                servicioAseoViasRegistroDto.AseoInformacionDTO.TramiteInformacionId = TramitePK;

                var AseoInformacion = _mapper.Map<AseoInformacion>(servicioAseoViasRegistroDto.AseoInformacionDTO);
                _context.Add(AseoInformacion);
            }

            await _context.SaveChangesAsync();

            return servicioAseoViasRegistroDto.NumeroDocumento;
        }
        else
        {
            return "Tramite inexistente";
        }
    }

    public async Task<string> RegistrarTramiteBienesInmuebles(BienesInmueblesRegistroDto bienesImueblesRegistroDto)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == bienesImueblesRegistroDto.TramiteId);

        int TramitePK = 0;
        if (tramite != null)
        {
            int currentYear = DateTime.UtcNow.Year;

            int counter = await _context.TramitesInformacion
                .Where(x => x.FechaCreacion.HasValue && x.FechaCreacion.Value.Year == currentYear)
                .CountAsync();

            counter++;

            bienesImueblesRegistroDto.NumeroDocumento = "MAT-IBI-DOC-"+ counter +"-"+ currentYear;

            var tramiteInformacion = _mapper.Map<TramiteInformacion>(bienesImueblesRegistroDto);
            _context.Add(tramiteInformacion);
            int FilaCreada = await _context.SaveChangesAsync();

            if (FilaCreada > 0)
            {
                TramitePK = tramiteInformacion.Id;
            }

            if (bienesImueblesRegistroDto.BienesInmueblesInformacionDTO != null)
            {
                bienesImueblesRegistroDto.BienesInmueblesInformacionDTO.TramiteInformacionId = TramitePK;

                var BienesInmueblesInformacion = _mapper.Map<BienesInmueblesInformacion>(bienesImueblesRegistroDto.BienesInmueblesInformacionDTO);
                _context.Add(BienesInmueblesInformacion);  
            }

            await _context.SaveChangesAsync();

            return bienesImueblesRegistroDto.NumeroDocumento;
        }
        else
        {
            return "Tramite inexistente";
        }
    }

    public async Task<ActionResult<List<ObtenerInformacionTramiteDTO>>> ObtenerTramiteAsync(
        ObtenerTramitesDTO obtenerTramitesDTO)
    {
        var tramite = await _context.TramitesInformacion.Where(x => x.TramiteId == obtenerTramitesDTO.IdTramiteCampos)
            .ToListAsync();

        if (tramite != null)
        {
            var tramiteInformacion = _mapper.Map<List<ObtenerInformacionTramiteDTO>>(tramite);

            return tramiteInformacion;
        }

        return null;
    }

    public async Task<ActionResult<List<ObtenerTramitesNombreIdDTO>>> ObtenerTramiteNombreIdAsync()
    {
        var tramite = await _context.Tramites.Where(x => x.estaActivo).ToListAsync();

        if (tramite != null)
        {
            var tramites = _mapper.Map<List<ObtenerTramitesNombreIdDTO>>(tramite);

            return tramites;
        }

        return null;
    }

    public async Task<string> CambiarEstadoTramiteAsync(CambiarEstadoTramiteDTO cambiarEstadoTramiteDTO)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == cambiarEstadoTramiteDTO.IdTramite);

        if (tramite != null)
        {
            if (cambiarEstadoTramiteDTO.Estado)
            {
                tramite.estaActivo = true;

                await _context.SaveChangesAsync();

                return "El tramite ahora se encuentra activo";
            }

            tramite.estaActivo = false;

            await _context.SaveChangesAsync();

            return "El tramite ahora se encuentra inactivo";
        }

        return "Tramite inexistente";
    }

    public async Task<string> ActualizarCamposDelTramiteAsync(int id, TramiteCampoRegisterDto tramiteCampoRegisterDto)
    {
        if (tramiteCampoRegisterDto == null) return "Datos invalidos";

        var tramite = _context.Tramites.FirstOrDefault(x => x.TramiteCampo.Id == id);

        if (tramite == null) return "Tramite inexsitente";

        var tramiteCamposExistente = await _context.TramiteCampos.FirstOrDefaultAsync(x => x.Id == id);

        if (tramiteCamposExistente != null)
        {
            _mapper.Map(tramiteCampoRegisterDto, tramiteCamposExistente);

            await _context.SaveChangesAsync();

            return "Tramites actualizados";
        }

        return "Tramite inexistente";
    }

    public async Task<string> ActualizarInformacionDelTramiteAsync(int id,
        TramiteInformacionActualizacionDTO tramiteInformacionActualizacionDTO)
    {
        if (tramiteInformacionActualizacionDTO == null) return "Datos invalidos";

        var tramiteInformacionExistente = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

        if (tramiteInformacionExistente != null)
        {
            tramiteInformacionActualizacionDTO.NumeroDocumento = tramiteInformacionExistente.NumeroDocumento;

            _mapper.Map(tramiteInformacionActualizacionDTO, tramiteInformacionExistente);

            await _context.SaveChangesAsync();

            return "Informacion de tramite actualizado";
        }

        return "Tramite inexistente";
    }

    public async Task<string> ActualizarTramiteBasuraAsync(int id, [FromBody] ServicioBasuraRegistroDto servicioBasuraRegistroDTO)
    {

        BasuraInformacionDto basuraInformacionDto = servicioBasuraRegistroDTO.BasuraInformacionDTO;

        if (servicioBasuraRegistroDTO == null || basuraInformacionDto == null) return "Datos inválidos";

        var tramiteBasura = await _context.BasuraInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);

        if (tramiteBasura == null)
        {
            throw new Exception("Este trámite no corresponde a un trámite de Inclusión/Exclusión de Basura");
        }

        try
        {
            var tramiteInformacionExistente = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

            if (tramiteInformacionExistente != null)
            {

                basuraInformacionDto.TramiteInformacionId = id;
                servicioBasuraRegistroDTO.TramiteId = tramiteInformacionExistente.TramiteId;
                servicioBasuraRegistroDTO.NumeroDocumento = tramiteInformacionExistente.NumeroDocumento;

                if (tramiteInformacionExistente != null)
                    _mapper.Map(servicioBasuraRegistroDTO, tramiteInformacionExistente);

                if (tramiteBasura != null)
                    _mapper.Map(basuraInformacionDto, tramiteBasura);

                await _context.SaveChangesAsync();

                return "Trámite actualizado";
            }
            else
            {
                throw new Exception("No existe tramite con el Id: " + id);
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error al actualizar trámite: " + ex.Message, ex);
        }
    }

    public async Task<string> ActualizarTramiteExoneracionAsync(int id, ExoneracionRegistroDto exoneracionRegistroDTO)
    {

        ExoneracionInformacionDto exoneracionInformacionDTO = exoneracionRegistroDTO.ExoneracionInformacionDTO;

        if (exoneracionRegistroDTO == null || exoneracionInformacionDTO == null) return "Datos inválidos";

        var tramiteExoneracion = await _context.ExoneracionesInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);

        if (tramiteExoneracion == null)
        {
            throw new Exception("Este trámite no corresponde a un trámite de Exoneración");
        }

        try
        {
            var tramiteInformacionExistente = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

            if (tramiteInformacionExistente != null)
            {

                exoneracionInformacionDTO.TramiteInformacionId = id;
                exoneracionRegistroDTO.TramiteId = tramiteInformacionExistente.TramiteId;
                exoneracionRegistroDTO.NumeroDocumento = tramiteInformacionExistente.NumeroDocumento;

                if (tramiteInformacionExistente != null)
                    _mapper.Map(exoneracionRegistroDTO, tramiteInformacionExistente);

                if (tramiteExoneracion != null)
                    _mapper.Map(exoneracionInformacionDTO, tramiteExoneracion);

                await _context.SaveChangesAsync();

                return "Trámite actualizado";
            }
            else
            {
                throw new Exception("No existe tramite con el Id: " + id);
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error al actualizar trámite: " + ex.Message, ex);
        }
    }

    public async Task<string> ActualizarTramiteAseoViasAsync(int id, ServicioAseoViasRegistroDto servicioAseoViasRegistroDTO)
    {

        AseoInformacionDto aseoInformacionDTO = servicioAseoViasRegistroDTO.AseoInformacionDTO;

        if (servicioAseoViasRegistroDTO == null || aseoInformacionDTO == null) return "Datos inválidos";

        var tramiteAseoVias = await _context.AseosInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);

        if (tramiteAseoVias == null)
        {
            throw new Exception("Este trámite no corresponde a un trámite de Inclusión/Exclusión de Aseo de Vías");
        }

        try
        {
            var tramiteInformacionExistente = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

            if (tramiteInformacionExistente != null)
            {

                aseoInformacionDTO.TramiteInformacionId = id;
                servicioAseoViasRegistroDTO.TramiteId = tramiteInformacionExistente.TramiteId;
                servicioAseoViasRegistroDTO.NumeroDocumento = tramiteInformacionExistente.NumeroDocumento;

                if (tramiteInformacionExistente != null)
                    _mapper.Map(servicioAseoViasRegistroDTO, tramiteInformacionExistente);

                if (tramiteAseoVias != null)
                    _mapper.Map(aseoInformacionDTO, tramiteAseoVias);

                await _context.SaveChangesAsync();

                return "Trámite actualizado";
            }
            else
            {
                throw new Exception("No existe tramite con el Id: " + id);
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error al actualizar trámite: " + ex.Message, ex);
        }
    }

    public async Task<string> ActualizarTramiteParquesOrnatoAsync(int id, ServicioParquesOrnatoRegistroDto servicioParquesOrnatoRegistroDTO)
    {

        ParqueInformacionDto parqueInformacionDTO = servicioParquesOrnatoRegistroDTO.ParqueInformacionDTO;

        if (servicioParquesOrnatoRegistroDTO == null || parqueInformacionDTO == null) return "Datos inválidos";

        var tramiteAseoVias = await _context.ParquesInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);

        if (tramiteAseoVias == null)
        {
            throw new Exception("Este trámite no corresponde a un trámite de Inclusión/Exclusión de Obras de Parques y Ornatos");
        }

        try
        {
            var tramiteInformacionExistente = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

            if (tramiteInformacionExistente != null)
            {

                parqueInformacionDTO.TramiteInformacionId = id;
                servicioParquesOrnatoRegistroDTO.TramiteId = tramiteInformacionExistente.TramiteId;
                servicioParquesOrnatoRegistroDTO.NumeroDocumento = tramiteInformacionExistente.NumeroDocumento;

                if (tramiteInformacionExistente != null)
                    _mapper.Map(servicioParquesOrnatoRegistroDTO, tramiteInformacionExistente);

                if (tramiteAseoVias != null)
                    _mapper.Map(parqueInformacionDTO, tramiteAseoVias);

                await _context.SaveChangesAsync();

                return "Trámite actualizado";
            }
            else
            {
                throw new Exception("No existe tramite con el Id: " + id);
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error al actualizar trámite: " + ex.Message, ex);
        }
    }

    public async Task<string> ActualizarTramiteBienesInmueblesAsync(int id, BienesInmueblesRegistroDto bienesInmueblesRegistroDTO)
    {
    
        BienesInmueblesInformacionDto bienesInmueblesInformacionDTO = bienesInmueblesRegistroDTO.BienesInmueblesInformacionDTO;

        if (bienesInmueblesRegistroDTO == null || bienesInmueblesInformacionDTO == null) return "Datos inválidos";

        var tramiteBienesInmuebles = await _context.BienesInmueblesInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);

        if (tramiteBienesInmuebles == null)
        {
            throw new Exception("Este trámite no corresponde a un trámite de Inclusión/Exclusión de Bienes Inmuebles");
        }

        try
        {
            var tramiteInformacionExistente = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

            if (tramiteInformacionExistente != null)
            {

                bienesInmueblesInformacionDTO.TramiteInformacionId = id;
                bienesInmueblesRegistroDTO.TramiteId = tramiteInformacionExistente.TramiteId;
                bienesInmueblesRegistroDTO.NumeroDocumento = tramiteInformacionExistente.NumeroDocumento;

                if (tramiteInformacionExistente != null)
                    _mapper.Map(bienesInmueblesRegistroDTO, tramiteInformacionExistente);

                if (tramiteBienesInmuebles != null)
                    _mapper.Map(bienesInmueblesInformacionDTO, tramiteBienesInmuebles);

                await _context.SaveChangesAsync();

                return "Trámite actualizado";
            }
            else
            {
                throw new Exception("No existe tramite con el Id: " + id);
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error al actualizar trámite: " + ex.Message, ex);
        }
    }

    public async Task<string> ActualizarTramiteAsync(int id, TramiteRegisterDto tramiteRegisterDto)
    {
        if (tramiteRegisterDto == null) return "Datos invaslidos";

        try
        {
            var tramiteExistente = await _context.Tramites.Include(t => t.TramiteCampo).FirstOrDefaultAsync(x => x.Id == id);
            
            var codigoExiste = await _context.Tramites.FirstOrDefaultAsync(x=>x.Codigo == tramiteRegisterDto.Codigo);

            if (codigoExiste != null)
            {
                if (codigoExiste.Id != id)
                {
                    return "El codigo ya existe";
                }
            }

            var nombreExiste = await _context.Tramites.FirstOrDefaultAsync(t => t.Nombre == tramiteRegisterDto.Nombre);
                
            if (nombreExiste != null)
            {
                if (nombreExiste.Id != id)
                {
                    return "El nombre ya existe";
                }
            }

            if (tramiteExistente != null)
            {

                _mapper.Map(tramiteRegisterDto, tramiteExistente);

                // Actualizar los campos del TramiteCampo existente si es necesario
                if (tramiteExistente.TramiteCampo != null)
                    _mapper.Map(tramiteRegisterDto.TramiteCampo, tramiteExistente.TramiteCampo);

                await _context.SaveChangesAsync();

                return "Tramite actualizado";
            }

            return "Tramite inexistente";
        }
        catch (Exception ex)
        {
            throw new Exception("Error al actualizar trámite: " + ex.Message, ex);
        }
    }

    public async Task<ObtenerInformacionTramiteUnico> ObtenerInformacionTramiteUnicoAsync(int id)
    {
        var tramite = await _context.Tramites.FirstOrDefaultAsync(x => x.Id == id);

        try
        {
            if (tramite != null)
            {

                var tramiteData = _mapper.Map<ObtenerInformacionTramiteUnico>(tramite);

                return tramiteData;
            }

            return null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener información del trámite: " + ex.Message, ex);
        }
    }

    public async Task<List<TramiteRegisterDto>> ObtenerTodosLosTramitesAsync()
    {
        try
        {
            // Asegúrate de incluir la entidad TramiteCampo al cargar los tramites
            var tramites = await _context.Tramites.Include(t => t.TramiteCampo)
                                               .Where(t => !t.esTramitePorDefecto)
                                               .ToListAsync();

            var tramitesDto = _mapper.Map<List<TramiteRegisterDto>>(tramites);

            return tramitesDto;
        }
        catch (Exception ex)
        {
            // Manejar excepciones específicas según sea necesario
            throw new Exception("Error al obtener tramites: " + ex.Message, ex);
        }
    }

    public async Task<ObtenerInformacionTramitePorIdDTO> ObtenerInformacionTramitePorIdAsync(int id)
    {
        try
        {
            // Asegúrate de incluir la entidad TramiteCampo al cargar los tramites
            var tramites = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

            var tramitesDto = _mapper.Map<ObtenerInformacionTramitePorIdDTO>(tramites);

            return tramitesDto;
        }
        catch (Exception ex)
        {
            // Manejar excepciones específicas según sea necesario
            throw new Exception("Error al obtener tramite: " + ex.Message, ex);
        }
    }

    public async Task<List<ObtenerTramiteInformacionConTramiteDTO>> ObtenerTodoTramiteInformacionAsync()
    {
        var tramiteInformacion = await _context.TramitesInformacion.Include(ti => ti.Tramite).ToListAsync();

        return _mapper.Map<List<ObtenerTramiteInformacionConTramiteDTO>>(tramiteInformacion);
    }

    public async Task<ExoneracionRegistroDto> ObtenerTramiteExoneracionInformacionPorIdAsync(int id)
    {
        var tramiteInformacion = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);
        if(tramiteInformacion == null)
        {
            throw new NullReferenceException("No se ha encontrado un tramite con el id: " + id);
        }

        var tramiteExoneracion = await _context.ExoneracionesInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);
        if(tramiteExoneracion == null)
        {
            throw new NullReferenceException("El tramite con el id: " + id + " no corresponde a un tramite de Exoneracion");
        }
        
        var exoneracionRegistroDto = _mapper.Map<ExoneracionRegistroDto>(tramiteInformacion);
        var exoneracionInformacionDto = _mapper.Map<ExoneracionInformacionDto>(tramiteExoneracion);
        exoneracionRegistroDto.ExoneracionInformacionDTO = exoneracionInformacionDto;

        return exoneracionRegistroDto;
    }

    public async Task<ServicioAseoViasRegistroDto> ObtenerTramiteServicioAseoViasInformacionPorIdAsync(int id)
    {
        var tramiteInformacion = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);
        if (tramiteInformacion == null)
        {
            throw new NullReferenceException("No se ha encontrado un tramite con el id: " + id);
        }

        var tramiteAseoVias = await _context.AseosInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);
        if (tramiteAseoVias == null)
        {
            throw new NullReferenceException("El tramite con el id: " + id + " no corresponde a un tramite de Aseo de Vias");
        }

        var servicioAseoViasRegistroDto = _mapper.Map<ServicioAseoViasRegistroDto>(tramiteInformacion);
        var aseoInformacionDto = _mapper.Map<AseoInformacionDto>(tramiteAseoVias);
        servicioAseoViasRegistroDto.AseoInformacionDTO = aseoInformacionDto;

        return servicioAseoViasRegistroDto;
    }

    public async Task<ServicioBasuraRegistroDto> ObtenerTramiteServicioBasuraInformacionPorIdAsync(int id)
    {
        var tramiteInformacion = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);
        if (tramiteInformacion == null)
        {
            throw new NullReferenceException("No se ha encontrado un tramite con el id: " + id);
        }

        var tramiteServicioBasura = await _context.BasuraInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);
        if (tramiteServicioBasura == null)
        {
            throw new NullReferenceException("El tramite con el id: " + id + " no corresponde a un tramite de Inclusion/Exclusion de Basura");
        }

        var servicioBasuraRegistroDto = _mapper.Map<ServicioBasuraRegistroDto>(tramiteInformacion);
        var basuraInformacionDto = _mapper.Map<BasuraInformacionDto>(tramiteServicioBasura);
        servicioBasuraRegistroDto.BasuraInformacionDTO = basuraInformacionDto;

        return servicioBasuraRegistroDto;
    }

    public async Task<ServicioParquesOrnatoRegistroDto> ObtenerTramiteServicioParqueOrnatoInformacionPorIdAsync(int id)
    {
        var tramiteInformacion = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);
        if (tramiteInformacion == null)
        {
            throw new NullReferenceException("No se ha encontrado un tramite con el id: " + id);
        }

        var parqueServicioBasura = await _context.ParquesInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);
        if (parqueServicioBasura == null)
        {
            throw new NullReferenceException("El tramite con el id: " + id + " no corresponde a un tramite de Obras de Parque y Ornato");
        }

        var servicioParquesOrnatoRegistroDto = _mapper.Map<ServicioParquesOrnatoRegistroDto>(tramiteInformacion);
        var parqueInformacionDto = _mapper.Map<ParqueInformacionDto>(parqueServicioBasura);
        servicioParquesOrnatoRegistroDto.ParqueInformacionDTO = parqueInformacionDto;

        return servicioParquesOrnatoRegistroDto;
    }

    public async Task<BienesInmueblesRegistroDto> ObtenerTramiteBienesInmueblesInformacionPorIdAsync(int id)
    {
        var tramiteInformacion = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);
        if (tramiteInformacion == null)
        {
            throw new NullReferenceException("No se ha encontrado un tramite con el id: " + id);
        }

        var bienesInmueblesTramite = await _context.BienesInmueblesInformacion.FirstOrDefaultAsync(x => x.TramiteInformacionId == id);
        if (bienesInmueblesTramite == null)
        {
            throw new NullReferenceException("El tramite con el id: " + id + " no corresponde a un tramite de inclusion/exlusion de Bienes Inmuebles");
        }

        var bienesInmueblesRegistroDto = _mapper.Map<BienesInmueblesRegistroDto>(tramiteInformacion);
        var bienesInmueblesInformacionDto = _mapper.Map<BienesInmueblesInformacionDto>(bienesInmueblesTramite);
        bienesInmueblesRegistroDto.BienesInmueblesInformacionDTO = bienesInmueblesInformacionDto;

        return bienesInmueblesRegistroDto;
    }



    public async Task<string> BorrarTramiteInformacionAsync(int id)
    {
        var tramiteInformacion = await _context.TramitesInformacion.FirstOrDefaultAsync(x => x.Id == id);

        if (tramiteInformacion != null)
        {
            _context.TramitesInformacion.Remove(tramiteInformacion);
            await _context.SaveChangesAsync();
            return "Informacion de tramite borrado exitosamente.";
        }

        return "Informacion de tramite no encontrada.";
    }
}