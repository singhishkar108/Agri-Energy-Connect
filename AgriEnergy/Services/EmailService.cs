using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace AgriEnergy.Services;
public class EmailService
{
    private readonly string smtpServer = "smtp.gmail.com";
    private readonly int smtpPort = 587;
    private readonly string fromEmail = "agrienergyconnect63@gmail.com";
    private readonly string appPassword = "azhg xsei oinw lqio"; // Use App Password here

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient(smtpServer)
        {
            Port = smtpPort,
            Credentials = new NetworkCredential(fromEmail, appPassword),
            EnableSsl = true,
        };

        var message = new MailMessage(fromEmail, toEmail, subject, body);
        await smtpClient.SendMailAsync(message);
    }
}