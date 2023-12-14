using ONPA.Common.Application;

namespace ONPA.Organizations.Api.Application.Commands;

public sealed record CreateOrganizationCommand : ApplicationCommandBase
{
    public string Name { get; init; }
    public string CardPrefix { get; init; }
    public string TaxId { get; init; }
    public string AccountNumber { get; init; }
    public string Address { get; init; }
    public string ContactEmail { get; init; }
    public string? Regon { get; init; }

    public CreateOrganizationCommand(string name, string cardPrefix, string taxId, string accountNumber, string address, string contactEmail, string? regon)
    {
        this.Name = name;
        this.CardPrefix = cardPrefix;
        this.TaxId = taxId;
        this.AccountNumber = accountNumber;
        this.Address = address;
        this.ContactEmail = contactEmail;
        this.Regon = regon;
    }
}