namespace GameServicesCloud.Mail;

public interface IMailTemplateService {
    string? Load(string name, Dictionary<string, string> variables);
}