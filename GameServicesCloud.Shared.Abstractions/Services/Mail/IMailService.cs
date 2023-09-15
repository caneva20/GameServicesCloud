namespace GameServicesCloud.Abstractions.Mail;

public interface IMailService {
    Task SendMail(string to, string subject, string htmlBody);
}