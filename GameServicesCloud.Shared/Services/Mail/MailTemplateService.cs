using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameServicesCloud.Mail;

public class MailTemplateService : IMailTemplateService {
    private readonly ILogger<MailTemplateService> _logger;

    private readonly MailTemplateOptions _options;

    private readonly Dictionary<string, string> _templates = new();

    public MailTemplateService(ILogger<MailTemplateService> logger, IOptions<MailTemplateOptions> options) {
        _logger = logger;
        _options = options.Value;

        LoadTemplates();
    }

    private void LoadTemplates() {
        _templates.Clear();

        var files = Directory.GetFiles(_options.Location, "*.html");

        foreach (var file in files) {
            _templates.Add(Path.GetFileName(file)[..^5], File.ReadAllText(file));
        }
    }

    public string? Load(string name, Dictionary<string, string> variables) {
        if (!_templates.TryGetValue(name, out var template)) {
            _logger.LogError("Failed to load template {Name} from {Location}", name, _options.Location);

            return null;
        }

        foreach (var (key, value) in variables) {
            template = template.Replace($"{{{{{key}}}}}", value);
        }

        return template;
    }
}