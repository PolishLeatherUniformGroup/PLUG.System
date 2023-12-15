namespace ONPA.Organizations.Contract.Responses;

public record OrganizationResponse(Guid Id, string Name, string CardPrefix, string Address, string TaxId, string AccountNumber, string Regon, string ContactEmail);