using GameServicesCloud.Abstractions.Mail;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace GameServicesCloud.Mail;

public class MailService : IMailService {
    private readonly ILogger<MailService> _logger;
    private readonly MailOptions _options;

    public MailService(ILogger<MailService> logger, IOptions<MailOptions> options) {
        _logger = logger;
        _options = options.Value;
    }

    public async Task SendMail(string to, string subject, string htmlBody) {
        using var client = new SmtpClient();

        _logger.LogInformation("Sending mail to {To}", to);

        try {
            await client.ConnectAsync(_options.Host, _options.Port);
            await client.AuthenticateAsync(_options.Login, _options.Password);

            var response = await client.SendAsync(new MimeMessage {
                From = { new MailboxAddress(_options.FromName, _options.FromEmail) },
                To = { new MailboxAddress(string.Empty, to) },
                Subject = subject,
                Body = new TextPart(TextFormat.Html) {
                    Text = htmlBody
                }
            });

            _logger.LogInformation("Mail to {To} finished with {Response}", to, response);

            await client.DisconnectAsync(true);
        } catch (Exception e) {
            _logger.LogError(e, "Sending mail to {To} failed", to);
        }
    }
}