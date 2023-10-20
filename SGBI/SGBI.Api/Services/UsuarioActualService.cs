using SGBI.SBGI.Core.Interfaces;

namespace SGBI.SGBI.API.Services;

public class UsuarioActualService : IUsuarioActualService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UsuarioActualService(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public string ObtenerNombreUsuarioActual()
    {
        return httpContextAccessor.HttpContext!.User.Identity!.Name!;
    }
}