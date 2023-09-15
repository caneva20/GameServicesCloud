namespace GameServicesCloud.Abstractions.Mail; 

public class MailOptions {
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string FromName { get; set; }
    public string FromEmail { get; set; }
}