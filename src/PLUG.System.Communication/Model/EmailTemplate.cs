namespace PLUG.System.Communication.Model;

public class EmailTemplate 
{
    public long Id { get; set; }
    public Guid OrganizationId { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public EmailTemplate(long id, Guid organizationId, string subject, string content)
    {
        this.Id = id;
        this.OrganizationId = organizationId;
        this.Subject = subject;
        this.Content = content;
    }
}