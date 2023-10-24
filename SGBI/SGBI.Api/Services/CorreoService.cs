using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using SGBI.SBGI.Core.Interfaces;
using SGBI.SGBI.API.Models;

namespace SGBI.SGBI.API.Services;

public class CorreoService: ICorreoService
{
    private readonly IConfiguration _configuration;


    public CorreoService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void EnviarCorreo(CorreoModel correo)
    {
        var emailMessage = new MimeMessage();
        var from = _configuration["EmailSettings:From"];

        emailMessage.From.Add(new MailboxAddress("Sistema de Gestión de Bienes Inmuebles", from));

        emailMessage.To.Add(new MailboxAddress(correo.Para, correo.Para));

        emailMessage.Subject = correo.Asunto;

        emailMessage.Body = new TextPart(TextFormat.Html)
        {
            Text = correo.Contenido
        };

        using var client = new SmtpClient();
        try
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true; //No se que hace esta linea pero sin esta madre no sirve

            client.Connect(_configuration["EmailSettings:SmtpServer"], 465, true);
            client.Authenticate(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
            client.Send(emailMessage);
        }
        catch (Exception ex) { throw; }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
    
}