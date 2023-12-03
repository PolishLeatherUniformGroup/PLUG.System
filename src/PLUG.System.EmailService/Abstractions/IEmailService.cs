namespace PLUG.System.EmailService.Abstractions;

public interface IEmailService
{
    Task SendMessage(string subject, string recipient, string content);
    
}