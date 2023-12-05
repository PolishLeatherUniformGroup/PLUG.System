using ONPA.Communication.Model;

namespace ONPA.Communication.Abstractions;

public interface ITemplateMapper
{
    EmailMessage Map(EmailTemplate template, params TemplateParam[] parameters);
}