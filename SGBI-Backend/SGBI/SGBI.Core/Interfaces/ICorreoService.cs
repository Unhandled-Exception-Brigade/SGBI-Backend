using SGBI.SGBI.API.Models;

namespace SGBI.SBGI.Core.Interfaces;

public interface ICorreoService
{
    void EnviarCorreo(CorreoModel correo);

}