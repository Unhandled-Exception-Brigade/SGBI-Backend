using SGBI.SGBI.Core.DTOs.Tramite;

namespace SGBI.SBGI.Core.Interfaces;

public interface ITramiteService
{
    Task<string> RegistraNuevoTramiteAsync(TramiteRegisterDto tramiteDto);
}