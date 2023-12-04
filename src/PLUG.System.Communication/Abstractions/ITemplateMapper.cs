using PLUG.System.Communication.Model;

namespace PLUG.System.Communication.Abstractions;

public interface ITemplateMapper
{
    EmailMessage Map(EmailTemplate template, params TemplateParam[] parameters);
}