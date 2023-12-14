namespace ONPA.Organizations.Contract.Requests;

public sealed record CreateOrganizationRequest(string Name, string CardPrefix, string TaxId, string AccountNumber, string Address, string ContactEmail, string? Regon)
{
}