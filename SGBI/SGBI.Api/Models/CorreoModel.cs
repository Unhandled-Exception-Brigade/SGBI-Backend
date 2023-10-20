namespace SGBI.SGBI.API.Models;

public class CorreoModel
{
    public CorreoModel(string para, string asunto, string contenido)
    {
        Para = para;
        Asunto = asunto;
        Contenido = contenido;
    }

    public string Para { get; set; }

    public string Asunto { get; set; }

    public string Contenido { get; set; }
}