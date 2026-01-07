using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace AgriEnergy.Services;

public class EmailService
{
    // Load configuration from environment variables
    private readonly string smtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER") ?? "your_smtp_server_here";
    private readonly int smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT") ?? "587");
    private readonly string fromEmail = Environment.GetEnvironmentVariable("FROM_EMAIL") ?? "your_email_here";
    private readonly string appPassword = Environment.GetEnvironmentVariable("APP_PASSWORD") ?? "your_app_password_here";

    // Alternatively, hardcode the configuration (not recommended for production)
    // private readonly string smtpServer = "your_smtp_server_here"; //Use SMTP Server here
    // private readonly int smtpPort = 587;
    // private readonly string fromEmail = "your_email_here"; //Use admin email here
    // private readonly string appPassword = "your_app_password_here"; //Use App Password here

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